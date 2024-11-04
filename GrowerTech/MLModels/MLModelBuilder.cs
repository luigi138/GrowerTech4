using System;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.IO;

namespace GrowerTech_MVC.MLModels
{
    public class MLModelBuilder
    {
        private static readonly string DataPath = Path.Combine(AppContext.BaseDirectory, "agricultural_data.csv");
        private static readonly string ModelPath = Path.Combine(AppContext.BaseDirectory, "MLModels", "AgriculturalRecommendationModel.zip");
        private readonly MLContext _mlContext;

        public MLModelBuilder()
        {
            _mlContext = new MLContext();
        }
        public void TrainModel()
        {
            IDataView data = _mlContext.Data.LoadFromTextFile<AgriculturalData>(
                path: DataPath,
                hasHeader: true,
                separatorChar: ',');

            var pipeline = _mlContext.Transforms.Concatenate("Features", "Temperature", "Humidity", "Rainfall")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", "RecommendedInput"))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(data);

            _mlContext.Model.Save(model, data.Schema, ModelPath);
            Console.WriteLine("O treino foi salvo！");
        }

        public string Predict(AgriculturalData input)
        {
            ITransformer trainedModel = _mlContext.Model.Load(ModelPath, out var modelInputSchema);
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<AgriculturalData, AgriculturalRecommendation>(trainedModel);
            var prediction = predictionEngine.Predict(input);
            return prediction.RecommendedInput;
        }
    }
}
