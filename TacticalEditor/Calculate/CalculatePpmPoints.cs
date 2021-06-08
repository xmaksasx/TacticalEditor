using System;
using TacticalEditor.Helpers;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAerodrome;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.Calculate
{
    class CalculatePpmPoints
    {
        private PpmPoint[] _ppmPoints;
        private readonly MeasureHelper _measureHelper = new MeasureHelper();
        private readonly CoordinateHelper _coordinateHelper = new CoordinateHelper();
        private AerodromePoint _aerodromePoint;

        public CalculatePpmPoints()
        {
            EventsHelper.PpmCollectionEvent += PpmCollectionChange;
            EventsHelper.ChangeAircraftCoordinateEvent += AircraftCoordinateChange;
            EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
        }

        private void ChangeAerodrome(AerodromePoint e)
        {
            _aerodromePoint = e;
        }

        private void AircraftCoordinateChange(AircraftPosition aircraft)
        {
	        for (int i = 0; i < _ppmPoints?.Length; i++)
	        {
		        if (_ppmPoints[i] == null) continue;

		        var ppmM = _ppmPoints[i].NavigationPoint;
		        var ppmG = _ppmPoints[i].NavigationPoint;
		        var acG = aircraft.GeoCoordinate;
		        var abpG = _aerodromePoint.NavigationPoint;

		        ppmM.Measure.Distance =
			        _measureHelper.GetDistanceInKmLatLon(acG.Latitude, acG.Longitude, ppmG.GeoCoordinate.Latitude,
				        ppmG.GeoCoordinate.Longitude);

		        ppmM.Measure.Psi =
			        _measureHelper.GetDegreesAzimuthLatLon(acG.Latitude, acG.Longitude, ppmG.GeoCoordinate.Latitude,
				        ppmG.GeoCoordinate.Longitude);

		        _coordinateHelper.LocalCordToXZ(abpG.GeoCoordinate.Latitude, abpG.GeoCoordinate.Longitude,
			        ppmG.GeoCoordinate.Latitude, ppmG.GeoCoordinate.Longitude,
			        out var x, out var z);

		        ppmG.GeoCoordinate.X = x - acG.X;
		        ppmG.GeoCoordinate.Z = z - acG.Z;

		        ppmM.Measure.Distance =
			        Math.Sqrt(Math.Pow(ppmG.GeoCoordinate.X, 2) + Math.Pow(ppmG.GeoCoordinate.Z, 2)) / 1000;

		        var time = (ppmM.Measure.Distance * 1000) / aircraft.V;
		        if (double.IsInfinity(time))
			        ppmM.Measure.RemainingTime = 0;
		        else
			        ppmM.Measure.RemainingTime = time;

		        if (2000 > Math.Sqrt(ppmG.GeoCoordinate.X * ppmG.GeoCoordinate.X +
		                             ppmG.GeoCoordinate.Z * ppmG.GeoCoordinate.Z))
			        if (ppmG.Executable == 1)
				        EventsHelper.OnChangeNpDEvent(new ChangeNp() {Action = 1, TypeOfNp = 2, IdNp = i + 2});

		        if (i == 0)
		        {
			        DebugParameters.XPPM = ppmG.GeoCoordinate.X;
			        DebugParameters.ZPPM = ppmG.GeoCoordinate.Z;
		        }
	        }
        }

        private void PpmCollectionChange(PpmPoint[] ppmPoint)
        {
            _ppmPoints = ppmPoint;
        }
    }
}
