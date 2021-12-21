using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace GoogleSpeechToTextAPI
{
    public static class GoogleSpeechToText
    {
        private const string FREE_API_KEY = "AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw"; // 白嫖用 API Key
        private static string API_KEY = FREE_API_KEY;
        private static readonly HttpClient httpClient = new HttpClient();

        public static void setAPIKey(string key)
        {
            API_KEY = key;
        }

        public static string speechToText(byte[] data, string format = "audio/x-flac", int sampleRate = 16000, string language = "zh-TW")
        {
            MediaTypeHeaderValue mediaType = new MediaTypeWithQualityHeaderValue(format);
            NameValueHeaderValue parameter = new NameValueHeaderValue("rate", sampleRate.ToString());
            mediaType.Parameters.Add(parameter);

            Uri uri = new Uri($"https://www.google.com/speech-api/v2/recognize?output=json&lang={language}&key={API_KEY}");
            using (Stream stream = new MemoryStream(data))
            {
                HttpContent content = new StreamContent(stream);
                content.Headers.ContentType = mediaType;

                string response = httpClient.PostAsync(uri, content).Result.Content.ReadAsStringAsync().Result;
                using (StringReader reader = new StringReader(response))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line[11] == ']')
                        {
                            continue;
                        }
                        SpeechToTextData json = new JavaScriptSerializer().Deserialize<SpeechToTextData>(line);
                        return json.getFirstTranscript();
                    }
                    return null;
                }
            }
        }
    }
}