using Microsoft.ML;
using Microsoft.ML.Data;
using GrowerTech_MVC.MLModels;
using System;
using System.IO;

namespace GrowerTech_MVC.Services
{
    public class MLModelService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model = null!;
        private const string ModelPath = "MLModels/agriculturalModel.zip"; 

        public MLModelService()
        {
            _mlContext = new MLContext();
            LoadModel(); 
        }

        
        public void TrainModel()
        {
            IDataView dataView = _mlContext.Data.LoadFromTextFile<AgriculturalData>(
                path: "GrowerTech4/agricultural_data.csv",
                hasHeader: true,
                separatorChar: ',');

            var dataProcessPipeline = _mlContext.Transforms.Concatenate("Features",
                nameof(AgriculturalData.Temperature),
                nameof(AgriculturalData.Humidity),
                nameof(AgriculturalData.SoilPH),
                nameof(AgriculturalData.Rainfall),
                nameof(AgriculturalData.CropType))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"));

            var trainer = _mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                labelColumnName: nameof(AgriculturalRecommendation.RecommendedInput),
                featureColumnName: "Features");
            
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            _model = trainingPipeline.Fit(dataView);
            _mlContext.Model.Save(_model, dataView.Schema, ModelPath);
        }

        public AgriculturalRecommendation Predict(AgriculturalData input)
        {
            if (_model == null)
            {
                LoadModel();
            }

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<AgriculturalData, AgriculturalRecommendation>(_model);
            return predictionEngine.Predict(input);
        }

        private void LoadModel()
        {
            if (File.Exists(ModelPath))
            {
                _model = _mlContext.Model.Load(ModelPath, out var modelInputSchema);
            }
            else
            {
                throw new FileNotFoundException("Model not found. Please train the model first.");
            }
        }
    }
}