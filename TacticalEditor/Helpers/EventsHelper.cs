using System.Windows.Shapes;
using TacticalEditor.Models;
using TacticalEditor.VisualObject.VisAerodrome;
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
        public delegate void ChangeAerodrome(AerodromePoint e); 
        public static event ChangeAerodrome ChangeAerodromeEvent;

        public static void OnChangeAerodromeEvent(AerodromePoint e) =>
            ChangeAerodromeEvent?.Invoke(e);

        #endregion

        #region Передает линию для отрисовки маршрута

        /// <summary>
        /// Передает линию для маршрута
        /// </summary>
        public delegate void AddVisualLine(Line visualLine);
        public static event AddVisualLine AddVisualLineEvent;
        public static void OnAddVisualLine(Line visualLine) =>
            AddVisualLineEvent?.Invoke(visualLine);

        #endregion

        #region Передает линию выходящую из последней точки

        /// <summary>
        /// Передает линию выходящую из последней точки
        /// </summary>
        public delegate void OutLineFromLastPoint(Line outLine);
        public static event OutLineFromLastPoint OutLineFromLastPointEvent;
        public static void OnOutLineFromLastPoint(Line outLine) =>
            OutLineFromLastPointEvent?.Invoke(outLine);

        #endregion

        #region Оповещает о смене размера карты

        /// <summary>
        /// Оповещает о смене размера карты
        /// </summary>
        public delegate void ChangeOfSize();
        public static event ChangeOfSize ChangeOfSizeEvent;
        public static void OnChangeOfSizeEvent() =>
            ChangeOfSizeEvent?.Invoke();

        #endregion

        #region Оповещает о смене позиции PPM

        /// <summary>
        /// Оповещает о смене позиции PPM
        /// </summary>
        public delegate void ChangePpmCoordinate(uint sizeMap);
        public static event ChangePpmCoordinate ChangePpmCoordinateEvent;
        public static void OnChangePpmCoordinateEvent(uint sizeMap) =>
            ChangePpmCoordinateEvent?.Invoke(sizeMap);

        #endregion

        #region Оповещает об изменении навигационной точки

        /// <summary>
        /// Оповещает об изменении навигационной точки
        /// </summary>
        /// <param name="changeNp"></param>
        public delegate void ChangeNpD (ChangeNp changeNp);
        public static event ChangeNpD ChangeNpDEvent;
        public static void OnChangeNpDEvent(ChangeNp changeNp) =>
	        ChangeNpDEvent?.Invoke(changeNp);

        #endregion

        #region Оповещает о позиции самолета

        /// <summary>
        /// Оповещает о позиции самолета
        /// </summary>
        public delegate void ChangeAircraftCoordinate(AircraftPosition aircraft);
        public static event ChangeAircraftCoordinate ChangeAircraftCoordinateEvent;
        public static void OnChangeAircraftCoordinateEvent(AircraftPosition aircraft) =>
            ChangeAircraftCoordinateEvent?.Invoke(aircraft);

        #endregion

        #region Передает коллекцию ППМ'ов

        /// <summary>
        /// Передает коллекцию ППМ'ов
        /// </summary>
        public delegate void PpmCollection(PpmPoint[] airPoints);
        public static event PpmCollection PpmCollectionEvent;
        public static void OnPpmCollectionEvent(PpmPoint[] airPoints) =>
            PpmCollectionEvent?.Invoke(airPoints);

        #endregion

        #region Передает коллекцию Аэродромов

        /// <summary>
        /// Передает коллекцию Аэродромов
        /// </summary>
        public delegate void AerodromeCollection(AerodromePoint[] aerodromePoints);
        public static event AerodromeCollection AerodromeCollectionEvent;
        public static void OnAerodromeCollectionEvent(AerodromePoint[] aerodromePoints) =>
            AerodromeCollectionEvent?.Invoke(aerodromePoints);

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
        public delegate void DebugNumber();
        public static event DebugNumber DebugNumberEvent;

        public static void OnDebugNumberEvent()
        {
            DebugNumberEvent?.Invoke();
        }

        #endregion
    }
}
