using System;
using System.IO;
using Microsoft.ML;
using SentimentServices.Models;

namespace SentimentServices.Services
{
    public static class SentimentService
    {
        static MLContext context = new MLContext();
        static ITransformer model
            = context.Model.Load(File.Open("C:/Users/amaro/source/repos/SentimentAnalysisApi/SentimentAnalysisApi/SentimentModel.zip", FileMode.Open));

        [ThreadStatic]
        static PredictionEngine<SourceData, Prediction> t_engine;

        private static PredictionEngine<SourceData, Prediction> GetPredictionEngine()
        {
            if (t_engine != null)
                return t_engine;

            return t_engine = model.CreatePredictionEngine<SourceData, Prediction>(context);
        }

        public static Prediction Predict(string text)
        {
            var engine = GetPredictionEngine();
            return engine.Predict(new SourceData { SentimentText = text });
        }
    }
}
