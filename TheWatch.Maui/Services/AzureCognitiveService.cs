using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using TheWatch.Maui.Data;
using Microsoft.Maui.Storage;

namespace TheWatch.Maui.Services
{
    public class AssistantModel
    {
        private AzureCognitiveService _cognitiveService;
        private string _email;
        private LocationModel _location;
        private string[] _phrases;

        public AssistantModel(AzureCognitiveService cognitiveService, string email, LocationModel location)
        {
            _cognitiveService = cognitiveService;
            _email = email;
            _location = location;
            LoadPhrases();
        }

        private void LoadPhrases()
        {
            // Load phrases from local storage
            PhraseOne = Preferences.Get("PhraseOne", "default phrase 1");
            PhraseTwo = Preferences.Get("PhraseTwo", "default phrase 2");
            PhraseThree = Preferences.Get("PhraseThree", "default phrase 3");
            _phrases = new[] { PhraseOne, PhraseTwo, PhraseThree };
        }

        public async Task StartListeningAsync()
        {
            while (true)
            {
                string recognizedText = await _cognitiveService.RecognizeSpeechAsync();
                if (Array.Exists(_phrases, phrase => recognizedText.Contains(phrase)))
                {
                    SendAlert(_email, _location);
                    break;
                }
            }
        }

        private void SendAlert(string email, LocationModel location)
        {
            // Code to send email and location to the WatchRequests table in the database
        }

        public string PhraseOne { get; private set; }
        public string PhraseTwo { get; private set; }
        public string PhraseThree { get; private set; }
    }

    public class AzureCognitiveService
    {
        private readonly string _subscriptionKey;
        private readonly string _region;

        public AzureCognitiveService(string subscriptionKey, string region)
        {
            _subscriptionKey = subscriptionKey;
            _region = region;
        }

        public async Task<string> RecognizeSpeechAsync()
        {
            var config = SpeechConfig.FromSubscription(_subscriptionKey, _region);
            using var recognizer = new SpeechRecognizer(config);

            var result = await recognizer.RecognizeOnceAsync();
            return result.Text;
        }
    }
}
