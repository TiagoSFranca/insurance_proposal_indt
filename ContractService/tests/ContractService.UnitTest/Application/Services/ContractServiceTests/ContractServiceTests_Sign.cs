using Bogus;
using ContractService.Domain;
using ContractService.Domain.Enums;
using NSubstitute.ExceptionExtensions;

namespace ContractService.UnitTest.Application.Services.ContractServiceTests;

public class ContractServiceTests_Create
{
    SignContractRequestBuilder _requestBuilder;
    ProposalResponseBuilder _proposalResponseBuilder;

    Faker _faker;

    private readonly IContractContext _context;
    private readonly IProposalRepository _proposalRepository;
    private readonly ILogger<ContractService.Application.Services.ContractService> _logger;

    ContractService.Application.Services.ContractService _sut;

    public ContractServiceTests_Create()
    {
        _requestBuilder = new SignContractRequestBuilder();
        _proposalResponseBuilder = new ProposalResponseBuilder()
            .WithIdStatus((int)EProposalStatus.Aproved);

        _faker = new Faker();

        _context = Substitute.For<IContractContext>();
        _proposalRepository = Substitute.For<IProposalRepository>();
        _logger = Substitute.For<ILogger<ContractService.Application.Services.ContractService>>();

        var setContracts = EmptyDbSet<Contract>();
        _context.Contracts.Returns(setContracts);

        _proposalRepository.GetById(Arg.Any<Guid>())
            .Returns(_proposalResponseBuilder.Build());

        _sut = new ContractService.Application.Services.ContractService(_context, _proposalRepository, _logger);
    }

    [Fact]
    public async Task Should_Return_Error_When_Request_Is_Null()
    {
        var result = await _sut.Sign(null);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.Empty(result.Messages);
            Assert.IsType<ArgumentNullException>(result.Exception);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Proposal_Is_Null()
    {
        var message = _faker.Lorem.Word();

        _proposalRepository.GetById(Arg.Any<Guid>())
            .Returns(Result<ProposalResponse>.Error(message));

        var result = await _sut.Sign(_requestBuilder.Build());

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(message, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Proposal_Is_Analysing()
    {
        var message = _faker.Lorem.Word();

        _proposalRepository.GetById(Arg.Any<Guid>())
            .Returns(_proposalResponseBuilder
                .WithIdStatus((int)EProposalStatus.Analyzing)
                .Build());

        var result = await _sut.Sign(_requestBuilder.Build());

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.ProposalAnalysing, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Proposal_Is_Rejected()
    {
        _proposalRepository.GetById(Arg.Any<Guid>())
            .Returns(_proposalResponseBuilder
                .WithIdStatus((int)EProposalStatus.Rejected)
                .Build());

        var result = await _sut.Sign(_requestBuilder.Build());

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.ProposalRejected, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Exists_Contract()
    {
        var request = _requestBuilder.Build();

        var setContracts = new List<Contract>()
        {
            new ContractBuilder()
                .WithIdProposal(request.IdProposal)
                .Build()
        }.AsDbSet();

        _context
            .Contracts
            .Returns(setContracts);

        var result = await _sut.Sign(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.ContractAlreadySigned, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Create()
    {
        var request = _requestBuilder.Build();

        var result = await _sut.Sign(request);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Messages);

            _context.Contracts.Received().Add(Arg.Any<Contract>());
            _context.Received().SaveChangesAsync();
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Ocurr_Error_While_Saving()
    {
        var request = _requestBuilder.Build();

        var message = _faker.Lorem.Word();

        _context.SaveChangesAsync(default)
            .Throws(new NullReferenceException(message));

        var result = await _sut.Sign(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.Empty(result.Messages);
            Assert.IsType<NullReferenceException>(result.Exception);
        });
    }
}
