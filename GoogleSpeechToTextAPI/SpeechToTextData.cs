namespace GoogleSpeechToTextAPI
{
    public class SpeechToTextData
    {
        public Result[] result
        {
            get;
            set;
        }

        public int result_index
        {
            get;
            set;
        }

        public string getFirstTranscript()
        {
            return result[0].alternative[0].transcript;
        }

        public double getConfidence()
        {
            return result[0].alternative[0].confidence;
        }
    }

    public class Result
    {
        public Alternative[] alternative
        {
            get;
            set;
        }

        public bool final
        {
            get;
            set;
        }
    }

    public class Alternative
    {
        public string transcript
        {
            get;
            set;
        }

        public double confidence
        {
            get;
            set;
        }
    }
}