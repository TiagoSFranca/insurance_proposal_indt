using ContractService.Domain;
using ContractService.Domain.Responses;
using ContractService.Domain.Responses.External;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ContractService.Persistence.Repositories;

public class ProposalExternalRepository : IProposalRepository
{
    private readonly JsonSerializerOptions _defaultOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ILogger<ProposalExternalRepository> _logger;

    public ProposalExternalRepository(HttpClient httpClient, ILogger<ProposalExternalRepository> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<ProposalResponse>> GetById(Guid id)
    {
        try
        {
            var httpResponse = await _httpClient.GetAsync($"proposals/{id}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                var message = Messages.ErrorWhileSearchingProposal;

                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    message = Messages.ProposalNotFound;

                return Result<ProposalResponse>.Error(message);
            }

            try
            {
                var content = await httpResponse.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<ProposalResponse>(content, _defaultOptions);

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deserializing result.");

                return Result<ProposalResponse>.Error(Messages.ErrorWhileSearchingProposal);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deserializing result.");

            return Result<ProposalResponse>.Error(Messages.ErrorWhileSearchingProposal);
        }
    }
}
