using Bogus;
using FluentValidation.Results;
using NSubstitute.ExceptionExtensions;

namespace ProposalService.UnitTest.Application.Services.ProposalServiceTests;

public class ProposalServiceTests_Create
{
    CreateProposalRequestBuilder _requestBuilder;

    Faker _faker;

    private readonly IProposalContext _context;
    private readonly IValidator<CreateProposalRequest> _createValidator;
    private readonly ILogger<ProposalService.Application.Services.ProposalService> _logger;

    ProposalService.Application.Services.ProposalService _sut;

    public ProposalServiceTests_Create()
    {
        _requestBuilder = new CreateProposalRequestBuilder();

        _faker = new Faker();

        _context = Substitute.For<IProposalContext>();
        _createValidator = Substitute.For<IValidator<CreateProposalRequest>>();
        _logger = Substitute.For<ILogger<ProposalService.Application.Services.ProposalService>>();

        _sut = new ProposalService.Application.Services.ProposalService(_context, _createValidator, _logger);
    }

    [Fact]
    public async Task Should_Return_Error_When_Request_Is_Null()
    {
        var result = await _sut.Create(null);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.Empty(result.Messages);
            Assert.IsType<ArgumentNullException>(result.Exception);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Request_Is_Invalid()
    {
        var errorMessage = _faker.Lorem.Word();

        _createValidator.Validate(Arg.Any<CreateProposalRequest>())
            .Returns(new FluentValidation.Results.ValidationResult()
            {
                Errors =
                [
                    new ValidationFailure("test", errorMessage)
                ]
            });

        var result = await _sut.Create(_requestBuilder.Build());

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(errorMessage, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Not_Found_InsuranceType()
    {
        var request = _requestBuilder.Build();

        var setInsuranceTypes = new List<InsuranceType>()
        {
            new InsuranceTypeBuilder()
            .WithId(request.IdInsuranceType + 1)
            .Build(),
        }.AsDbSet();

        _context.InsuranceTypes.Returns(setInsuranceTypes);

        var result = await _sut.Create(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.InsuranceTypeNotFound, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Not_Found_PaymentMethod()
    {
        var request = _requestBuilder.Build();

        var setInsuranceTypes = new List<InsuranceType>()
        {
            new InsuranceTypeBuilder()
            .WithId(request.IdInsuranceType)
            .Build(),
        }.AsDbSet();

        _context.InsuranceTypes.Returns(setInsuranceTypes);

        var setPayments = new List<PaymentMethod>()
        {
            new PaymentMethodBuilder()
            .WithId(request.IdPaymentMethod + 1)
            .Build(),
        }.AsDbSet();

        _context.PaymentMethods.Returns(setPayments);

        var result = await _sut.Create(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Messages);
            Assert.Equal(Messages.PaymentMethodNotFound, result.Messages[0]);
        });
    }

    [Fact]
    public async Task Should_Create_Client_When_Not_Found()
    {
        var request = _requestBuilder.Build();

        var setInsuranceTypes = new List<InsuranceType>()
        {
            new InsuranceTypeBuilder()
            .WithId(request.IdInsuranceType)
            .Build(),
        }.AsDbSet();

        _context.InsuranceTypes.Returns(setInsuranceTypes);

        var setPayments = new List<PaymentMethod>()
        {
            new PaymentMethodBuilder()
            .WithId(request.IdPaymentMethod)
            .Build(),
        }.AsDbSet();

        _context.PaymentMethods.Returns(setPayments);

        var setClients = EmptyDbSet<Client>();

        _context.Clients.Returns(setClients);

        var result = await _sut.Create(request);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Messages);

            _context.Clients.Received().Add(Arg.Any<Client>());
            _context.Received().SaveChangesAsync();
        });
    }

    [Fact]
    public async Task Should_Create()
    {
        var request = _requestBuilder.Build();

        var setInsuranceTypes = new List<InsuranceType>()
        {
            new InsuranceTypeBuilder()
            .WithId(request.IdInsuranceType)
            .Build(),
        }.AsDbSet();

        _context.InsuranceTypes.Returns(setInsuranceTypes);

        var setPayments = new List<PaymentMethod>()
        {
            new PaymentMethodBuilder()
            .WithId(request.IdPaymentMethod)
            .Build(),
        }.AsDbSet();

        _context.PaymentMethods.Returns(setPayments);

        var setClients = new List<Client>()
        {
            new ClientBuilder()
                .WithId(request.IdClient)
                .Build(),
        }.AsDbSet();

        _context.Clients.Returns(setClients);

        var result = await _sut.Create(request);

        Assert.Multiple(() =>
        {
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Messages);

            _context.Clients.DidNotReceive().Add(Arg.Any<Client>());
            _context.Received().SaveChangesAsync();
        });
    }

    [Fact]
    public async Task Should_Return_Error_When_Ocurr_Error_While_Saving()
    {
        var request = _requestBuilder.Build();

        var setInsuranceTypes = new List<InsuranceType>()
        {
            new InsuranceTypeBuilder()
            .WithId(request.IdInsuranceType)
            .Build(),
        }.AsDbSet();

        _context.InsuranceTypes.Returns(setInsuranceTypes);

        var setPayments = new List<PaymentMethod>()
        {
            new PaymentMethodBuilder()
            .WithId(request.IdPaymentMethod)
            .Build(),
        }.AsDbSet();

        _context.PaymentMethods.Returns(setPayments);

        var setClients = EmptyDbSet<Client>();

        _context.Clients.Returns(setClients);

        var message = _faker.Lorem.Word();

        _context.SaveChangesAsync(default)
            .Throws(new NullReferenceException(message));

        var result = await _sut.Create(request);

        Assert.Multiple(() =>
        {
            Assert.False(result.IsSuccess);
            Assert.Empty(result.Messages);
            Assert.IsType<NullReferenceException>(result.Exception);
        });
    }
}
