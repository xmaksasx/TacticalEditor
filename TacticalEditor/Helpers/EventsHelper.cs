using System.Windows.Shapes;
using TacticalEditor.Models;
using TacticalEditor.Models.NavPoint;
using TacticalEditor.VisualObject.VisAirport;
using TacticalEditor.VisualObject.VisPpm;

namespace TacticalEditor.Helpers
{
    class EventsHelper
    {
        #region Передает аэропорт при его смене
        /// <summary>
        /// Смена аэропорта
        /// </summary>
        /// <param name="e"></param>
        public delegate void ChangeAirport(AirBasePoint e); 
        public static event ChangeAirport ChangeAirportEvent;

        public static void OnChangeAirportEvent(AirBasePoint e) =>
            ChangeAirportEvent?.Invoke(e);

        #endregion

        #region Передает линию для отрисовки маршрута
      
        /// <summary>
        /// Передает линию для отрисовки маршрута
        /// </summary>
        /// <param name="e"></param>
        public delegate void AddVisualLine(Line visualLine);
        public static event AddVisualLine AddVisualLineEvent;
        public static void OnAddVisualLine(Line visualLine) =>
            AddVisualLineEvent?.Invoke(visualLine);

        #endregion

        #region Передает линию выходящую из последней точки

        /// <summary>
        /// Передает линию выходящую из последней точки
        /// </summary>
        /// <param name="e"></param>
        public delegate void OutLineFromLastPoint(Line outLine);
        public static event OutLineFromLastPoint OutLineFromLastPointEvent;
        public static void OnOutLineFromLastPoint(Line outLinee) =>
            OutLineFromLastPointEvent?.Invoke(outLinee);

        #endregion

        #region Оповещает о смене размера карты

        /// <summary>
        /// Передает линию для отрисовки маршрута
        /// </summary>
        /// <param name="e"></param>
        public delegate void ChangeOfSize(uint sizeMap);
        public static event ChangeOfSize ChangeOfSizeEvent;
        public static void OnChangeOfSizeEvent(uint sizeMap) =>
            ChangeOfSizeEvent?.Invoke(sizeMap);

        #endregion

        #region Оповещает о смене позиции PPM

        /// <summary>
        /// Оповещает о смене позиции PPM
        /// </summary>
        /// <param name="e"></param>
        public delegate void ChangePpmCoordinate(uint sizeMap);
        public static event ChangePpmCoordinate ChangePpmCoordinateEvent;
        public static void OnChangePpmCoordinateEvent(uint sizeMap) =>
            ChangePpmCoordinateEvent?.Invoke(sizeMap);

        #endregion

        #region Оповещает о позиции самолета

        /// <summary>
        /// Оповещает о позиции самолета
        /// </summary>
        /// <param name="e"></param>
        public delegate void ChangeAircraftCoordinate(AircraftPosition aircraft);
        public static event ChangeAircraftCoordinate ChangeAircraftCoordinateEvent;
        public static void OnChangeAircraftCoordinateEvent(AircraftPosition aircraft) =>
            ChangeAircraftCoordinateEvent?.Invoke(aircraft);

        #endregion

        #region Передает коллекцию ППМ'ов

        /// <summary>
        /// Передает коллекцию ППМ'ов
        /// </summary>
        /// <param name="e"></param>
        public delegate void PpmCollection(PpmPoint[] airPoints);
        public static event PpmCollection PpmCollectionEvent;
        public static void OnPpmCollectionEvent(PpmPoint[] airPoints) =>
            PpmCollectionEvent?.Invoke(airPoints);

        #endregion

        #region Передает коллекцию Аэродромовв

        /// <summary>
        /// Передает коллекцию ППМ'ов
        /// </summary>
        /// <param name="e"></param>
        public delegate void AirBaseCollection(AirBasePoint[] airBases);
        public static event AirBaseCollection AirBaseCollectionEvent;
        public static void OnAirBaseCollectionEvent(AirBasePoint[] airBases) =>
            AirBaseCollectionEvent?.Invoke(airBases);

        #endregion

        #region Оповещает о построении коробочки
        /// <summary>
        /// Оповещает о построении коробочки
        /// </summary>
        public delegate void BuildBox();
        public static event BuildBox BuildBoxEvent;

        public static void OnBuildBoxEvent()
        {
            BuildBoxEvent?.Invoke();
        }

        #endregion


        #region Передает состояние режима
        /// <summary>
        /// Передает состояние режима
        /// </summary>
        /// <param name="e"></param>
        public delegate void MenuStatus(MenuStates e);
        public static event MenuStatus MenuStatusEvent;

        public static void OnMenuStatusEvent(MenuStates e)
        {
            MenuStatusEvent?.Invoke(e);
        }

        #endregion

        #region Передает состояние режима
        /// <summary>
        /// Передает состояние режима
        /// </summary>
        /// <param name="e"></param>
        public delegate void Destroy();
        public static event Destroy DestroyEvent;

        public static void OnDestroyEvent()
        {
            DestroyEvent?.Invoke();
        }

        #endregion



        #region Передает состояние режима
        /// <summary>
        /// Передает состояние режима
        /// </summary>
        /// <param name="e"></param>
        public delegate void DebugNumber(double e);
        public static event DebugNumber DebugNumberEvent;

        public static void OnDebugNumberEvent(double e)
        {
            DebugNumberEvent?.Invoke(e);
        }

        #endregion
    }
}
