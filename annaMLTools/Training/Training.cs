using Microsoft.ML;
using Microsoft.ML.Trainers;
using Reforce_annaBotML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnaMLTools.Training
{
    public class Training
    {
        public static string MODEL_PATH = @"D:\Reforce ANNA\reforce_annaBotML.Model\MLModel.zip";
        public static string DATA_PATH;

        private static IEstimator<ITransformer> BuildTrainingPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("INTENTION", "INTENTION")
                                      .Append(mlContext.Transforms.Text.FeaturizeText("PHRASE_tf", "PHRASE"))
                                      .Append(mlContext.Transforms.CopyColumns("Features", "PHRASE_tf"))
                                      .Append(mlContext.Transforms.NormalizeMinMax("Features", "Features"))
                                      .AppendCacheCheckpoint(mlContext);
            // Set the training algorithm
            var trainer = mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(new LbfgsMaximumEntropyMulticlassTrainer.Options() { L2Regularization = 0.1254188f, L1Regularization = 0.5540954f, OptimizationTolerance = 1E-07f, HistorySize = 50, MaximumNumberOfIterations = 745766220, InitialWeightsDiameter = 0.2798961f, DenseOptimizer = false, LabelColumnName = "INTENTION", FeatureColumnName = "Features" })
                                      .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel"));

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline;
        }

        public static void RetrainModel(string modelPath, string dataPath)
        {
            MLContext mlContext = new MLContext();

            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                    path: DATA_PATH,
                    hasHeader: true,
                    separatorChar: ',',
                    allowQuoting: true,
                    trimWhitespace: false
                );

            IEstimator<ITransformer> pipeline = BuildTrainingPipeline(mlContext);

            ITransformer trainer = pipeline.Fit(dataView);

            mlContext.Model.Save(trainer, dataView.Schema, MODEL_PATH);
        }
    }
}
