using Dadata.Model;

namespace RandomAPI.Models
{
    public class DadataResult
    {
        public long ElapsedTime { get; }
        public IEnumerable<SuggestResponse<Bank>> DadataResponses { get; }

        public DadataResult(IEnumerable<SuggestResponse<Bank>> dadataResponses, long elapsedTime)
        {
            DadataResponses = dadataResponses;
            ElapsedTime = elapsedTime;
        }
    }
}

