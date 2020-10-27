using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MapWorld;

namespace TacticalEditor.Helpers
{
    class MapHelper
    {
        public uint SizeMap { get; set; }
        private readonly Grid _grid;
        private readonly ScrollViewer _scrollViewer;
        private readonly BaseMap _baseA = new BaseMap();
        private double _hOff;
        private double _vOff;
        private Point _scrollMousePoint;
        private int _levelOfDetail;

        public MapHelper(Grid grid, ScrollViewer scrollViewer, int levelOfDetail)
        {
            _levelOfDetail = levelOfDetail;
            _grid = grid;
            _scrollViewer = scrollViewer;
            _scrollViewer.PreviewMouseRightButtonUp += ScrollViewer_PreviewMouseRightButtonUp;
            _scrollViewer.PreviewMouseRightButtonDown += ScrollViewer_PreviewMouseRightButtonDown;
            _scrollViewer.PreviewMouseMove += ScrollViewer_PreviewMouseMove;
            _scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
            _scrollViewer.PreviewMouseWheel += ScrollViewer_PreviewMouseWheel;
            GridResize();
        }

        private void GridResize()
        {
            SizeMap = MapSize(_levelOfDetail);
            int rowCol = (int) Math.Pow(2, _levelOfDetail);
            _grid.Height = SizeMap;
            _grid.Width = SizeMap;
            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();
            for (int i = 0; i < rowCol; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                _grid.ColumnDefinitions.Add(col);
                RowDefinition row = new RowDefinition();
                _grid.RowDefinitions.Add(row);
            }

            ClearUnVisibility();
            ZoomMap();
        }

        private void ZoomMap()
        {
            uint sizeMap = MapSize(_levelOfDetail);
            if (_grid.ActualHeight < sizeMap)
            {
                var point = Mouse.GetPosition(_grid);
                var vOffset = (long) (point.Y + _scrollViewer.ContentVerticalOffset);
                var hOffset = (long) (point.X + _scrollViewer.ContentHorizontalOffset);
                _scrollViewer.ScrollToVerticalOffset(vOffset);
                _scrollViewer.ScrollToHorizontalOffset(hOffset);
            }

            if (_grid.ActualHeight > sizeMap)
            {
                var point = Mouse.GetPosition(_grid);
                var vOffset = (long)(_scrollViewer.ContentVerticalOffset - point.Y / 2);
                var hOffset = (long) (_scrollViewer.ContentHorizontalOffset - point.X / 2);
                _scrollViewer.ScrollToVerticalOffset(vOffset);
                _scrollViewer.ScrollToHorizontalOffset(hOffset);
            }
        }

        private void ClearUnVisibility()
        {
            int vOffset = (int)_scrollViewer.VerticalOffset;
            int hOffset = (int) _scrollViewer.HorizontalOffset;
            int aH = (int) _scrollViewer.ActualHeight;
            int aW = (int)_scrollViewer.ActualWidth;

            var rowFirst = vOffset / 256;
            var colFirst = hOffset / 256;
            var rowLast = (aH + vOffset) / 256;
            var colLast = (aW + hOffset) / 256;
            int rowCol = (int)(_grid.Height / 256);

            if (rowLast < rowCol)
                rowLast = rowLast + 1;
            else
                rowLast = rowCol;

            if (colLast < rowCol)
                colLast = colLast + 1;
            else
                colLast = rowCol;

            _grid.Children.Clear();
          
            for (int i = rowFirst; i < rowLast; i++)
            {
                for (int j = colFirst; j < colLast; j++)
                {
                    Image tb = new Image();
                    var path = "MapWorld.MapWorld.Size" + _levelOfDetail + ".os_" + j + "_" + i + "_" + _levelOfDetail + ".png";
                    tb.Source = _baseA.GetPartMap(path);
                   // tb.Visibility = Visibility.Hidden;
                    Grid.SetRow(tb, i);
                    Grid.SetColumn(tb, j);
                    _grid.Children.Add(tb);
                }
            }
        }

        private uint MapSize(int levelOfDetail)
        {
            return (uint)256 << levelOfDetail;
        }

        private void ScrollViewer_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _scrollViewer.ReleaseMouseCapture();
        }

        private void ScrollViewer_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _scrollMousePoint = e.GetPosition(_scrollViewer);
            _hOff = _scrollViewer.HorizontalOffset;
            _vOff = _scrollViewer.VerticalOffset;
            _scrollViewer.CaptureMouse();
        }
      
        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_scrollViewer.IsMouseCaptured)
            {
                _scrollViewer.ScrollToHorizontalOffset(_hOff + (_scrollMousePoint.X - e.GetPosition(_scrollViewer).X));
                _scrollViewer.ScrollToVerticalOffset(_vOff + (_scrollMousePoint.Y - e.GetPosition(_scrollViewer).Y));
            }


            return;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
           ClearUnVisibility();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e != null)
            {
                int zoom = e.Delta > 0 ? 1 : -1;
                _levelOfDetail += zoom;
                if (_levelOfDetail < 2)
                    _levelOfDetail = 2;
                if (_levelOfDetail > 10)
                    _levelOfDetail = 10;
                GridResize();
            }
        }
    }
}
