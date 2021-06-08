using TacticalEditor.Helpers;
using TacticalEditor.VisualObject.VisAerodrome;
using TacticalEditor.VisualObject.VisAircraft;


namespace TacticalEditor.WorkingPoints
{
	class AircraftWorker
	{
		private AerodromePoint _currentAerodrome;

		public AircraftWorker()
		{
			EventsHelper.ChangeAerodromeEvent += ChangeAerodrome;
		}

		private void ChangeAerodrome(AerodromePoint e)
		{
			_currentAerodrome = e;
		}

		public VisualAircraft CrateVisualAircraft()
		{
			return new VisualAircraft(PrepareAircraftPoint(), _currentAerodrome);
		}

		private AircraftPoint PrepareAircraftPoint()
		{
			AircraftPoint aircraftPoint = new AircraftPoint();
			aircraftPoint.NavigationPoint.GeoCoordinate.Latitude = _currentAerodrome.AerodromeInfo.Runway.Threshold.Latitude;
			aircraftPoint.NavigationPoint.GeoCoordinate.Longitude = _currentAerodrome.AerodromeInfo.Runway.Threshold.Longitude;
			aircraftPoint.NavigationPoint.Measure.Psi = _currentAerodrome.AerodromeInfo.Runway.Heading;
			return aircraftPoint;
		}
	}
}