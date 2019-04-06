using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SentimentMediator.ViewModels;
using SentimentServices.Services;

namespace SentimentMediator
{
    public class SentimentRequestHandler : IRequestHandler<SentimentRequest, SentimentResponse>
    {
        public async Task<SentimentResponse> Handle(SentimentRequest request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
                new SentimentResponse
                {
                    Message = request.Message,
                    Score = SentimentService.Predict(request.Message).Percentage
                }
            );
        }
    }
}
