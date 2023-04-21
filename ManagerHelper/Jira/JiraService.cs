using RestSharp;

namespace ManagerHelper.Jira
{
    internal class JiraService
    {
        private readonly IRestClient _restClient;

        /// <summary>
        /// Note: This assumes that the rest client is ready to go.
        /// </summary>
        /// <param name="restClient"></param>
        public JiraService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<string> GetIssueAsync(string key)
        {
            var request = new RestRequest($"rest/api/2/issue/{key}", Method.Get);
            var response = await _restClient.ExecuteAsync(request);
            return response.Content;
        }
    }
}
