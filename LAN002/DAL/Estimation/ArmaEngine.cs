﻿//using ABMath.ModelFramework.Data;
//using ABMath.ModelFramework.Models;
//using lib12.Collections;
//using lib12.DependencyInjection;
using System.Collections.Generic;
using TemperatureEstimator.Entities;

namespace TemperatureEstimator.EstimationEngines
{
    //[Singleton]
    public class ArmaEngine //: IEstimationEngine
    {
        //private readonly ARMAModel armaModel;

        //public Estimator Estimator { get { return Estimator.ARMA; } }
        //public string EstimatorName { get { return "ARMA"; } }
        //public string EstimatorInfo { get { return "https://en.wikipedia.org/wiki/Autoregressive%E2%80%93moving-average_model"; } }

        //public ArmaEngine()
        //{
        //    armaModel = new ARMAModel(4, 2);
        //}

        //public EstimationResult Estimate(IEnumerable<IDateValue> dateValues)
        //{
        //    var series = new TimeSeries();
        //    dateValues.ForEach(x => series.Add(x.Date, x.Value, true));

        //    armaModel.SetInput(0, series, null);
        //    armaModel.FitByMLE(200, 100, 0, null);
        //    armaModel.ComputeResidualsAndOutputs();

        //    var result = armaModel.GetOutput(3) as TimeSeries;
        //    return EstimationResult.Create(result[0], this);
        //}
    }
}