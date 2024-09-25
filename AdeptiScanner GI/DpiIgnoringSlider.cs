using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdeptiScanner_GI
{
    internal record struct TickData(int Value, int X);

    public partial class DpiIgnoringSlider : Control
    {

        private Color m_SliderColor = Color.FromKnownColor(KnownColor.Highlight);
        private Color m_CenterBarColor = Color.DarkGray;
        private Color m_TickColor = Color.DarkGray;
        private int m_TickInterval = 1;
        private int m_MinValue = -1;
        private int m_MaxValue = 1;
        private int m_Value = 0;

        private const int m_SliderWidthHalf = 5;
        private const int m_TickLineWidth = 3;
        private const int m_CenterLineWidth = 3;

        private int m_WidthConversionFactor;
        private List<TickData> m_TickLocations = new List<TickData>();

        private Brush m_SliderBrush;
        private Brush m_CenterBarBrush;
        private Brush m_TickBrush;

        private Pen m_CenterBarPen;
        private Pen m_TickPen;

        private bool m_MouseIsDown;

        #region events

        private static readonly object EVENT_SCROLL = new object();

        public event EventHandler Scroll
        {
            add => Events.AddHandler(EVENT_SCROLL, value);
            remove => Events.RemoveHandler(EVENT_SCROLL, value);
        }

        protected virtual void OnScroll(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[EVENT_SCROLL];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion // events

        #region properties
        public int TickInterval
        {
            get => m_TickInterval; 
            set
            {
                if (m_TickInterval == value)
                {
                    return;
                }
                m_TickInterval = value;
                UpdateConversionFactorAndTickLocations();
                Invalidate();
            }
        }

        public int MinValue
        {
            get => m_MinValue;
            set
            {
                if (m_MinValue == value)
                {
                    return;
                }
                m_MinValue = value;
                UpdateConversionFactorAndTickLocations();
                Invalidate();
            }
        }

        public int MaxValue
        {
            get => m_MaxValue;
            set
            {
                if (m_MaxValue == value)
                {
                    return;
                }
                m_MaxValue = value;
                UpdateConversionFactorAndTickLocations();
                Invalidate();
            }
        }

        public int Value
        {
            get => m_Value;
            set
            {
                if (m_Value == value) 
                { 
                    return; 
                }
                m_Value = value;
                Invalidate();
                OnScroll(EventArgs.Empty);
            }
        }

        #endregion // properties

        public DpiIgnoringSlider()
        {
            InitializeComponent();
            DoubleBuffered = true;

            m_SliderBrush = new SolidBrush(m_SliderColor);
            m_CenterBarBrush = new SolidBrush(m_CenterBarColor);
            m_TickBrush = new SolidBrush(m_TickColor);

            m_CenterBarPen = new Pen(m_CenterBarBrush, m_CenterLineWidth);
            m_TickPen = new Pen(m_TickBrush, m_TickLineWidth);
            UpdateConversionFactorAndTickLocations();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateConversionFactorAndTickLocations();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_MouseIsDown)
            {
                UpdateFromMouseEvent(e);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (m_MouseIsDown)
            {
                m_MouseIsDown = false;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (m_MouseIsDown)
            {
                UpdateFromMouseEvent(e);
            }
            m_MouseIsDown = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            UpdateFromMouseEvent(e);
            m_MouseIsDown = true;
        }

        private void UpdateFromMouseEvent(MouseEventArgs e)
        {
            var x = e.Location.X;

            var closest = m_TickLocations.First();
            var closestDist = Math.Abs(closest.X - x);

            foreach (var location in m_TickLocations)
            {
                var dist = Math.Abs(location.X - x);
                if (dist < closestDist)
                {
                    closest = location;
                    closestDist = dist;
                }
            }

            Value = closest.Value;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var quarterHeight = Height / 4;

            pe.Graphics.DrawLine(m_CenterBarPen, new Point(m_TickLocations.First().X, quarterHeight * 2), new Point(m_TickLocations.Last().X, quarterHeight * 2));
            foreach (var tick in m_TickLocations)
            {
                pe.Graphics.DrawLine(m_TickPen, new Point(tick.X, quarterHeight * 3), new Point(tick.X, quarterHeight * 4));
            }

            var sliderCenter = TranslateValueToXCoord(m_Value);
            pe.Graphics.FillPolygon(m_SliderBrush, new[] {
                new Point(sliderCenter - m_SliderWidthHalf, 0),
                new Point(sliderCenter - m_SliderWidthHalf, quarterHeight * 2),
                new Point(sliderCenter, quarterHeight * 3),
                new Point(sliderCenter + m_SliderWidthHalf, quarterHeight * 2),
                new Point(sliderCenter + m_SliderWidthHalf, 0),
            });

            
        }

        protected int TranslateValueToXCoord(int value)
        {
            return m_SliderWidthHalf + (value - m_MinValue) * m_WidthConversionFactor;
        }

        protected void UpdateConversionFactorAndTickLocations()
        {
            m_WidthConversionFactor = (Width - m_SliderWidthHalf * 2) / (m_MaxValue - m_MinValue);

            var list = new List<TickData> ();

            for (int tickvalue = m_MinValue; tickvalue <= m_MaxValue; tickvalue += m_TickInterval)
            {
                var tickX = TranslateValueToXCoord(tickvalue);
                list.Add(new TickData(tickvalue, tickX));
            }

            var translatedMax = TranslateValueToXCoord(m_MaxValue);
            var maxTick = new TickData(m_MaxValue, translatedMax);
            if (!list.Contains(maxTick))
            {
                list.Add(maxTick);
            }

            m_TickLocations = list;
        }
    }
}
