using Bogus;
using NSubstitute.ExceptionExtensions;
using ProposalService.Domain.Enums;

namespace ProposalService.UnitTest.Application.Services.ProposalServiceTests;

public class ProposalServiceTests_UpdateStatus
{
    private Guid id;
    private int idStatus;

    Faker _faker;

    private readonly IProposalContext _context;
    private readonly IValidator<CreateProposalRequest> _createValidator;
    private readonly ILogger<ProposalService.Application.Services.ProposalService> _logger;

    ProposalService.Application.Services.ProposalService _sut;

    public ProposalServiceTests_UpdateStatus()
    {
        _faker = new Faker();
        id = Guid.NewGuid();
        idStatus = (int)EProposalStatus.Aproved;

        _context = Substitute.For<IProposalContext>();
        _createValidator = Substitute.For<IValidator<CreateProposalRequest>>();
        _logger = Substitute.For<ILogger<ProposalService.Application.Services.ProposalService>>();

        var setProposal = new List<Proposal>()
        {
            new ProposalBuilder()
                .WithId(id)
                .Build()
        }.AsDbSet();

        _context.Proposals.Returns(setProposal);

        var setProposalStatus = new List<ProposalStatus>()
        {
            new ProposalStatusBuilder()
                .WithId(idStatus)
                .Build()
        }.AsDbSet();

        _context.ProposalStatuses.Returns(setProposalStatus);

        _sut = new ProposalService.Application.Services.ProposalService(_context, _createValidator, _logger);
    }

    [Fact]
    public async Task Should_Return_Error_When_Not_Found_Proposal()
    {
        var setProposal = EmptyDbSet<Proposal>();

        _context.Proposals.Returns(setProposal);

        var result = await _sut.UpdateStatus(id, idStatus);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.ProposalNotFound, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Not_Found_ProposalStatus()
    {
        var setProposalStatus = EmptyDbSet<ProposalStatus>();

        _context.ProposalStatuses.Returns(setProposalStatus);

        var result = await _sut.UpdateStatus(id, idStatus);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.ProposalStatusNotFound, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Proposal_Status_Isnt_Analysing()
    {
        var setProposal = new List<Proposal>()
        {
            new ProposalBuilder()
                .WithId(id)
                .WithIdStatus((int)EProposalStatus.Rejected)
                .Build()
        }.AsDbSet();

        _context.Proposals.Returns(setProposal);

        var result = await _sut.UpdateStatus(id, idStatus);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.ProposalCantChangeStatus, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Proposal_Has_Same_Status()
    {
        idStatus = (int) EProposalStatus.Analyzing;

        var setProposal = new List<Proposal>()
        {
            new ProposalBuilder()
                .WithId(id)
                .WithIdStatus(idStatus)
                .Build()
        }.AsDbSet();

        _context.Proposals.Returns(setProposal);

        var setProposalStatus = new List<ProposalStatus>()
        {
            new ProposalStatusBuilder()
                .WithId(idStatus)
                .Build()
        }.AsDbSet();

        _context.ProposalStatuses.Returns(setProposalStatus);

        var result = await _sut.UpdateStatus(id, idStatus);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.ProposalWithSameStatus, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_UpdateStatus()
    {
        var result = await _sut.UpdateStatus(id, idStatus);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Messages);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Ocurr_Error_While_Saving()
    {
        var message = _faker.Lorem.Word();

        _context.SaveChangesAsync(default)
            .Throws(new NullReferenceException(message));

        var result = await _sut.UpdateStatus(id, idStatus);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.Empty(result.Messages);
            Assert.IsType<NullReferenceException>(result.Exception);
        });
    }
}
