using RestSharp;

namespace API.Interfaces;

public interface IRequestManager
{
    IRestResponse SendRequest(Method method, string endPoint, object body = null, Dictionary<string, string> header = null);

    IRestResponse SendRequest(IRestRequest request);

    byte[] DownloadData(IRestRequest request);
}