using System.Collections.Generic;
using System.Windows.Shapes;
using TacticalEditor.Models;
using TacticalEditor.Models.Points;

namespace TacticalEditor.Helpers
{
    class EventsHelper
    {
        #region Передает аэропорт при его смене
        /// <summary>
        /// Смена аэропорта
        /// </summary>
        /// <param name="e"></param>
        public delegate void ChangeAirport(AirportPoint e); 
        public static event ChangeAirport ChangeAirportEvent;

        public static void OnChangeAirportEvent(AirportPoint e) =>
            ChangeAirportEvent?.Invoke(e);

        #endregion

        #region Передает линию для отрисовки маршрута
      
        /// <summary>
        /// Передает линию для отрисовки маршрута
        /// </summary>
        /// <param name="e"></param>
        public delegate void AddLineToRoute(Line oldLine, Line newLine);
        public static event AddLineToRoute AddLineToRouteEvent;
        public static void OnAddLineToRoute(Line oldLine, Line newLine) =>
            AddLineToRouteEvent?.Invoke(oldLine, newLine);

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

        #region Передает коллекцию ППМ'ов

        /// <summary>
        /// Передает коллекцию ППМ'ов
        /// </summary>
        /// <param name="e"></param>
        public delegate void PpmCollection(InfoPoint[] airPoints);
        public static event PpmCollection PpmCollectionEvent;
        public static void OnPpmCollectionEvent(InfoPoint[] airPoints) =>
            PpmCollectionEvent?.Invoke(airPoints);

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
    }
}
