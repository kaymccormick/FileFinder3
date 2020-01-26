﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using AppShared.Interfaces ;
using NLog;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace WpfApp1.Controls
{
    /// <summary>
    /// Interaction logic for SystemParametersControl.xaml
    /// </summary>
    public partial class SystemParametersControl
        : UserControl,
            ISettingsPanel
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SystemParametersControl()
        {
            InitializeComponent();

            var sysProp = new SysProp();
            dynamic sysObj = new ExpandoObject();
            var t = typeof(SystemParameters);
            var resKeyProps = t.GetProperties().Where( info => info.PropertyType == typeof(ResourceKey) );
            var propertyDefinitionCollection = new PropertyDefinitionCollection();
            foreach ( var resKeyProp in resKeyProps )
            {
                var propertyInfo = typeof(SysProp).GetProperty( resKeyProp.Name );
                Debug.Assert( propertyInfo != null );
                propertyInfo.SetValue( sysProp, resKeyProp.GetValue( null ) );


                var r = new Regex( @"Key$" );
                var barePropName = r.Replace( resKeyProp.Name, "" );

                var LabelStr = barePropName;
                var bareProp = t.GetProperty( barePropName, BindingFlags.Static | BindingFlags.Public );
                if ( bareProp == null )
                {
                    Logger.Warn( $"no prop for {resKeyProp.Name}" );
                }
                else
                {
                    var propSysProp = typeof(SysProp).GetProperty( barePropName );
                    Debug.Assert( propSysProp != null );
                    propSysProp.SetValue( sysProp, bareProp.GetValue( null ) );

                    var p = new PropertyDefinition { TargetProperties = { barePropName } };
                    propertyDefinitionCollection.Add( p );

                    var dict = sysObj as IDictionary < string, object >;
                    dict[barePropName] = bareProp.GetValue( null );
                }
            }


            var propertyGrid = new PropertyGrid
                               {
                                   AutoGenerateProperties = false,
                                   SelectedObject         = sysProp,
                                   PropertyDefinitions    = propertyDefinitionCollection
                               };
            Content                          = propertyGrid;
            propertyGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            propertyGrid.VerticalAlignment   = VerticalAlignment.Stretch;

            //Label label = new Label() {Content = LabelStr,};
        }
    }
}

namespace WpfApp1
{
    public class SysProp
    {
        /// <summary>Gets the width, in pixels, of the left and right edges of the focus rectangle.  </summary>
        /// <returns>The edge width.</returns>
        [ Category( "Appearance" ) ]
        public double FocusBorderWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the height, in pixels, of the upper and lower edges of the focus rectangle.  </summary>
        /// <returns>The edge height.</returns>
        [ Category( "Appearance" ) ]
        public double FocusBorderHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets information about the High Contrast accessibility feature. </summary>
        /// <returns>
        /// <see langword="true" /> if the HIGHCONTRASTON option is selected; otherwise,<see langword=" false" />.</returns>
        [ Category( "Accessibility" ) ]
        public bool HighContrast { [ SecurityCritical ] get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FocusBorderWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FocusBorderWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FocusBorderHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FocusBorderHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.HighContrast" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey HighContrastKey { get; set; }

        /// <summary>Gets a value indicating whether the drop shadow effect is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> if the drop shadow effect is enabled; otherwise, <see langword="false" />.</returns>
        /// 
        [ Category( "Effects" ) ]
        public bool DropShadow { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether native menus appear as a flat menu.  </summary>
        /// <returns>
        /// <see langword="true" /> if the flat menu appearance is set; otherwise, <see langword="false" />.</returns>
        /// 
        [ Category( "Appearance" ) ]
        public bool FlatMenu { [ SecurityCritical ] get; set; }

        /// <summary>Gets the size of the work area on the primary display monitor. </summary>
        /// <returns>A <see langword="RECT" /> structure that receives the work area coordinates, expressed as virtual screen coordinates.</returns>
        [ Category( "Layout" ) ]
        public Rect WorkArea { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.DropShadow" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey DropShadowKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FlatMenu" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FlatMenuKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.WorkArea" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey WorkAreaKey { get; set; }

        /// <summary>Gets the width, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view. </summary>
        /// <returns>The width of an icon cell.</returns>
        [ Category( "Layout" ) ]
        public double IconHorizontalSpacing { get; set; }

        /// <summary>Gets the height, in pixels, of an icon cell. The system uses this rectangle to arrange icons in large icon view. </summary>
        /// <returns>The height of an icon cell.</returns>
        [ Category( "Layout" ) ]
        public double IconVerticalSpacing { get; set; }

        /// <summary>Gets a value indicating whether icon-title wrapping is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> if icon-title wrapping is enabled; otherwise <see langword="false" />.</returns>
        [ Category( "Appearance" ) ]
        public bool IconTitleWrap { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IconHorizontalSpacing" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IconHorizontalSpacingKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IconVerticalSpacing" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IconVerticalSpacingKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IconTitleWrap" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IconTitleWrapKey { get; set; }

        /// <summary>Gets a value indicating whether menu access keys are always underlined. </summary>
        /// <returns>
        /// <see langword="true" /> if menu access keys are always underlined; <see langword="false" /> if they are underlined only when the menu is activated by the keyboard.</returns>
        [ Category( "Input" ) ]
        public bool KeyboardCues { [ SecurityCritical ] get; set; }

        /// <summary>Gets the keyboard repeat-delay setting, which is a value in the range from 0 (approximately 250 milliseconds delay) through 3 (approximately 1 second delay). </summary>
        /// <returns>The keyboard repeat-delay setting.</returns>
        [ Category( "Input" ) ]
        public int KeyboardDelay { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether the user relies on the keyboard instead of the mouse, and whether the user wants applications to display keyboard interfaces that are typically hidden. </summary>
        /// <returns>
        /// <see langword="true" /> if the user relies on the keyboard; otherwise,<see langword=" false" />.</returns>
        [ Category( "Input" ) ]
        public bool KeyboardPreference { [ SecurityCritical ] get; set; }

        /// <summary>Gets the keyboard repeat-speed setting, which is a value in the range from 0 (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second). </summary>
        /// <returns>The keyboard repeat-speed setting.</returns>
        [ Category( "Input" ) ]
        public int KeyboardSpeed { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether the snap-to-default button is enabled. If enabled, the mouse cursor automatically moves to the default button of a dialog box, such as OK or Apply.  </summary>
        /// <returns>
        /// <see langword="true" /> when the feature is enabled; otherwise, <see langword="false" />.</returns>
        [ Category( "Input" ) ]
        public bool SnapToDefaultButton { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the number of lines to scroll when the mouse wheel is rotated. </summary>
        /// <returns>The number of lines.</returns>
        [ Category( "Input" ) ]
        public int WheelScrollLines { [ SecurityCritical ] get; set; }

        /// <summary>Gets the time, in milliseconds, that the mouse pointer must remain in the hover rectangle to generate a mouse-hover event.  </summary>
        /// <returns>The time, in milliseconds, that the mouse must be in the hover rectangle to generate a mouse-hover event.</returns>
        [ Category( "Input" ) ]
        public TimeSpan MouseHoverTime { get; set; }

        /// <summary>Gets the height, in pixels, of the rectangle within which the mouse pointer has to stay to generate a mouse-hover event. </summary>
        /// <returns>The height of a rectangle used for a mouse-hover event.</returns>
        [ Category( "Input" ) ]
        public double MouseHoverHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width, in pixels, of the rectangle within which the mouse pointer has to stay to generate a mouse-hover event.  </summary>
        /// <returns>The width of a rectangle used for a mouse-hover event.</returns>
        [ Category( "Input" ) ]
        public double MouseHoverWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.KeyboardCues" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey KeyboardCuesKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.KeyboardDelay" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey KeyboardDelayKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.KeyboardPreference" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey KeyboardPreferenceKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.KeyboardSpeed" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey KeyboardSpeedKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SnapToDefaultButton" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SnapToDefaultButtonKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.WheelScrollLines" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey WheelScrollLinesKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MouseHoverTime" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MouseHoverTimeKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MouseHoverHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MouseHoverHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MouseHoverWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MouseHoverWidthKey { get; set; }

        /// <summary>Gets a value indicating whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu item. </summary>
        /// <returns>
        /// <see langword="true" /> if left-aligned; otherwise, <see langword="false" />.</returns>
        [ Category( "Appearance" ) ]
        public bool MenuDropAlignment { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether menu fade animation is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> when fade animation is enabled; otherwise, <see langword="false" />.</returns>
        /// 
        [ Category( "Effects" ) ]
        public bool MenuFade { [ SecurityCritical ] get; set; }

        /// <summary>Gets the time, in milliseconds, that the system waits before displaying a shortcut menu when the mouse cursor is over a submenu item.  </summary>
        /// <returns>The delay time.</returns>
        [ Category( "Input" ) ]
        public int MenuShowDelay { [ SecurityCritical ] get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuDropAlignment" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuDropAlignmentKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuFade" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuFadeKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuShowDelay" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuShowDelayKey { get; set; }

        /// <summary>Gets the system value of the <see cref="P:System.Windows.Controls.Primitives.Popup.PopupAnimation" /> property for combo boxes. </summary>
        /// <returns>A pop-up animation value.</returns>
        [ Category( "Effects" ) ]
        public PopupAnimation ComboBoxPopupAnimation { get; set; }

        /// <summary>Gets a value indicating whether the slide-open effect for combo boxes is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> for enabled; otherwise, <see langword="false" />.</returns>
        [ Category( "Effects" ) ]
        public bool ComboBoxAnimation { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether the client area animation feature is enabled.</summary>
        /// <returns>A Boolean value; true if client area animation is enabled, false otherwise.</returns>
        [ Category( "Effects" ) ]
        public bool ClientAreaAnimation { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether the cursor has a shadow around it. </summary>
        /// <returns>
        /// <see langword="true" /> if the shadow is enabled; otherwise, <see langword="false" />.</returns>
        public bool CursorShadow { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether the gradient effect for window title bars is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> if the gradient effect is enabled; otherwise, <see langword="false" />.</returns>
        [ Category( "Effects" ) ]
        public bool GradientCaptions { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> if hot tracking is enabled; otherwise, <see langword="false" />.</returns>
        [ Category( "Effects" ) ]
        public bool HotTracking { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether the smooth-scrolling effect for list boxes is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> if the smooth-scrolling effect is enabled; otherwise, <see langword="false" />.</returns>
        [ Category( "Effects" ) ]
        public bool ListBoxSmoothScrolling { [ SecurityCritical ] get; set; }

        /// <summary>Gets the system value of the <see cref="P:System.Windows.Controls.Primitives.Popup.PopupAnimation" /> property for menus. </summary>
        /// <returns>The pop-up animation property.</returns>
        [ Category( "Effects" ) ]
        public PopupAnimation MenuPopupAnimation { get; set; }

        /// <summary>Gets a value indicating whether the menu animation feature is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> if menu animation is enabled; otherwise, <see langword="false" />.</returns>
        [ Category( "Effects" ) ]
        public bool MenuAnimation { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether the selection fade effect is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> if the fade effect is enabled; otherwise, <see langword="false" />.</returns>
        public bool SelectionFade { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether hot tracking of a stylus is enabled.  </summary>
        /// <returns>
        /// <see langword="true" /> if hot tracking of a stylus is enabled; otherwise <see langword="false" />.</returns>
        [ Category( "Effects" ) ]
        public bool StylusHotTracking { [ SecurityCritical ] get; set; }

        /// <summary>Gets the system value of the <see cref="P:System.Windows.Controls.Primitives.Popup.PopupAnimation" /> property for ToolTips. </summary>
        /// <returns>A system value for the pop-up animation property.</returns>
        [ Category( "Effects" ) ]
        public PopupAnimation ToolTipPopupAnimation { get; set; }

        /// <summary>Gets a value indicating whether <see cref="T:System.Windows.Controls.ToolTip" /> animation is enabled.  </summary>
        /// <returns>
        /// <see langword="true" /> if ToolTip animation is enabled; otherwise, <see langword="false" />.</returns>
        public bool ToolTipAnimation { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether ToolTip animation uses a fade effect or a slide effect.  </summary>
        /// <returns>
        /// <see langword="true" /> if a fade effect is used; <see langword="false" /> if a slide effect is used.</returns>
        [ Category( "Effects" ) ]
        public bool ToolTipFade { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether all user interface (UI) effects are enabled.   </summary>
        /// <returns>
        /// <see langword="true" /> if all UI effects are enabled; <see langword="false" /> if they are disabled.</returns>
        [ Category( "Effects" ) ]
        public bool UIEffects { [ SecurityCritical ] get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ComboBoxAnimation" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ComboBoxAnimationKey { get; set; }

        /// <summary>Gets a <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ClientAreaAnimation" /> property.</summary>
        /// <returns>A resource key.</returns>
        [ Category( "Effects" ) ]
        public ResourceKey ClientAreaAnimationKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.CursorShadow" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey CursorShadowKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.GradientCaptions" /> property. </summary>
        /// <returns>A resource key.</returns>
        [ Category( "Effects" ) ]
        public ResourceKey GradientCaptionsKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.HotTracking" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey HotTrackingKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ListBoxSmoothScrolling" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ListBoxSmoothScrollingKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuAnimation" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuAnimationKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SelectionFade" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SelectionFadeKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.StylusHotTracking" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey StylusHotTrackingKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ToolTipAnimation" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ToolTipAnimationKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ToolTipFade" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ToolTipFadeKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.UIEffects" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey UIEffectsKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ComboBoxPopupAnimation" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ComboBoxPopupAnimationKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuPopupAnimation" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuPopupAnimationKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ToolTipPopupAnimation" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ToolTipPopupAnimationKey { get; set; }

        /// <summary>Gets the animation effects associated with user actions. </summary>
        /// <returns>
        /// <see langword="true" /> if the minimize window animations feature is enabled; otherwise,<see langword=" false" />.</returns>
        [ Category( "Effects" ) ]
        public bool MinimizeAnimation { [ SecurityCritical ] get; set; }

        /// <summary>Gets the border multiplier factor that determines the width of a window's sizing border. </summary>
        /// <returns>A multiplier.</returns>
        [ Category( "Appearance" ) ]
        public int Border { [ SecurityCritical ] get; set; }

        /// <summary>Gets the caret width, in pixels, for edit controls. </summary>
        /// <returns>The caret width.</returns>
        [ Category( "Metrics" ) ]
        public double CaretWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value indicating whether dragging of full windows is enabled. </summary>
        /// <returns>
        /// <see langword="true" /> if dragging of full windows is enabled; otherwise, <see langword="false" />.</returns>
        [ Category( "Effects" ) ]
        public bool DragFullWindows { [ SecurityCritical ] get; set; }

        /// <summary>Gets the number of times the Set Foreground Window flashes the taskbar button when rejecting a foreground switch request.</summary>
        /// <returns>A flash count.</returns>
        [ Category( "Effects" ) ]
        public int ForegroundFlashCount { [ SecurityCritical ] get; set; }

        /// <summary>Gets the metric that determines the border width of the nonclient area of a nonminimized window. </summary>
        /// <returns>A border width.</returns>
        [ Category( "Metrics" ) ]
        public double BorderWidth { get; set; }

        /// <summary>Gets the metric that determines the scroll width of the nonclient area of a nonminimized window. </summary>
        /// <returns>The scroll width, in pixels.</returns>
        [ Category( "Metrics" ) ]
        public double ScrollWidth { get; set; }

        /// <summary>Gets the metric that determines the scroll height of the nonclient area of a nonminimized window. </summary>
        /// <returns>The scroll height, in pixels.</returns>
        [ Category( "Metrics" ) ]
        public double ScrollHeight { get; set; }

        /// <summary>Gets the metric that determines the caption width for the nonclient area of a nonminimized window. </summary>
        /// <returns>The caption width.</returns>
        [ Category( "Metrics" ) ]
        public double CaptionWidth { get; set; }

        /// <summary>Gets the metric that determines the caption height for the nonclient area of a nonminimized window. </summary>
        /// <returns>The caption height.</returns>
        [ Category( "Metrics" ) ]
        public double CaptionHeight { get; set; }

        /// <summary>Gets the metric that determines the width of the small caption of the nonclient area of a nonminimized window. </summary>
        /// <returns>The caption width, in pixels.</returns>
        [ Category( "Metrics" ) ]
        public double SmallCaptionWidth { get; set; }

        /// <summary>Gets the metric that determines the height of the small caption of the nonclient area of a nonminimized window. </summary>
        /// <returns>The caption height, in pixels.</returns>
        [ Category( "Metrics" ) ]
        public double SmallCaptionHeight { get; set; }

        /// <summary>Gets the metric that determines the width of the menu. </summary>
        /// <returns>The menu width, in pixels.</returns>
        [ Category( "Metrics" ) ]
        public double MenuWidth { get; set; }

        /// <summary>Gets the metric that determines the height of the menu. </summary>
        /// <returns>The menu height.</returns>
        [ Category( "Metrics" ) ]
        public double MenuHeight { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimizeAnimation" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimizeAnimationKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.Border" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey BorderKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.CaretWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey CaretWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ForegroundFlashCount" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ForegroundFlashCountKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.DragFullWindows" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey DragFullWindowsKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.BorderWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey BorderWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ScrollWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ScrollWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ScrollHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ScrollHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.CaptionWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey CaptionWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.CaptionHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey CaptionHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SmallCaptionWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SmallCaptionWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuHeightKey { get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a horizontal window border. </summary>
        /// <returns>The height of a border.</returns>
        [ Category( "Metrics" ) ]
        public double ThinHorizontalBorderHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of a vertical window border. </summary>
        /// <returns>The width of a border.</returns>
        [ Category( "Metrics" ) ]
        public double ThinVerticalBorderWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width, in pixels, of a cursor. </summary>
        /// <returns>The cursor width.</returns>
        [ Category( "Metrics" ) ]
        public double CursorWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the height, in pixels, of a cursor. </summary>
        /// <returns>The cursor height.</returns>
        [ Category( "Metrics" ) ]
        public double CursorHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a 3-D border.   </summary>
        /// <returns>The height of a border.</returns>
        [ Category( "Metrics" ) ]
        public double ThickHorizontalBorderHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of a 3-D border.   </summary>
        /// <returns>The width of a border.</returns>
        [ Category( "Metrics" ) ]
        public double ThickVerticalBorderWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width of a rectangle centered on a drag point to allow for limited movement of the mouse pointer before a drag operation begins.  </summary>
        /// <returns>The width of the rectangle, in pixels.</returns>
        public double MinimumHorizontalDragDistance { [ SecurityCritical ] get; set; }

        /// <summary>Gets the height of a rectangle centered on a drag point to allow for limited movement of the mouse pointer before a drag operation begins.  </summary>
        /// <returns>The height of the rectangle, in pixels.</returns>
        public double MinimumVerticalDragDistance { [ SecurityCritical ] get; set; }

        /// <summary>Gets the height of the horizontal border of the frame around a window. </summary>
        /// <returns>The border height.</returns>
        public double FixedFrameHorizontalBorderHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width of the vertical border of the frame around a window. </summary>
        /// <returns>The border width.</returns>
        public double FixedFrameVerticalBorderWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the height of the upper and lower edges of the focus rectangle.  </summary>
        /// <returns>The edge height.</returns>
        public double FocusHorizontalBorderHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width of the left and right edges of the focus rectangle.  </summary>
        /// <returns>The edge width.</returns>
        public double FocusVerticalBorderWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width, in pixels, of the client area for a full-screen window on the primary display monitor.  </summary>
        /// <returns>The width of the client area.</returns>
        public double FullPrimaryScreenWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the height, in pixels, of the client area for a full-screen window on the primary display monitor.  </summary>
        /// <returns>The height of the client area.</returns>
        public double FullPrimaryScreenHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width, in pixels, of the arrow bitmap on a horizontal scroll bar. </summary>
        /// <returns>The width of the arrow bitmap.</returns>
        public double HorizontalScrollBarButtonWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the height of a horizontal scroll bar, in pixels. </summary>
        /// <returns>The height of the scroll bar.</returns>
        public double HorizontalScrollBarHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width, in pixels, of the <see cref="T:System.Windows.Controls.Primitives.Thumb" /> in a horizontal scroll bar. </summary>
        /// <returns>The width of the thumb.</returns>
        public double HorizontalScrollBarThumbWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the default width of an icon. </summary>
        /// <returns>The icon width.</returns>
        public double IconWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the default height of an icon. </summary>
        /// <returns>The icon height.</returns>
        public double IconHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets the width of a grid that a large icon will fit into. </summary>
        /// <returns>The grid width.</returns>
        public double IconGridWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets the height of a grid in which a large icon will fit. </summary>
        /// <returns>The grid height.</returns>
        public double IconGridHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of a maximized top-level window on the primary display monitor.  </summary>
        /// <returns>The window width.</returns>
        public double MaximizedPrimaryScreenWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a maximized top-level window on the primary display monitor.  </summary>
        /// <returns>The window height.</returns>
        public double MaximizedPrimaryScreenHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the maximum width, in pixels, of a window that has a caption and sizing borders.  </summary>
        /// <returns>The maximum window width.</returns>
        public double MaximumWindowTrackWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the maximum height, in pixels, of a window that has a caption and sizing borders.  </summary>
        /// <returns>The maximum window height.</returns>
        public double MaximumWindowTrackHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of the default menu check-mark bitmap.  </summary>
        /// <returns>The width of the bitmap.</returns>
        public double MenuCheckmarkWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of the default menu check-mark bitmap.  </summary>
        /// <returns>The height of a bitmap.</returns>
        public double MenuCheckmarkHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of a menu bar button.  </summary>
        /// <returns>The width of a menu bar button.</returns>
        public double MenuButtonWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a menu bar button.  </summary>
        /// <returns>The height of a menu bar button.</returns>
        public double MenuButtonHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the minimum width, in pixels, of a window.  </summary>
        /// <returns>The minimum width of a window.</returns>
        public double MinimumWindowWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the minimum height, in pixels, of a window.  </summary>
        /// <returns>The minimum height of a window.</returns>
        public double MinimumWindowHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of a minimized window.  </summary>
        /// <returns>The width of a minimized window.</returns>
        public double MinimizedWindowWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a minimized window.  </summary>
        /// <returns>The height of a minimized window.</returns>
        public double MinimizedWindowHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of a grid cell for a minimized window.  </summary>
        /// <returns>The width of a grid cell for a minimized window.</returns>
        public double MinimizedGridWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a grid cell for a minimized window.  </summary>
        /// <returns>The height of a grid cell for a minimized window.</returns>
        public double MinimizedGridHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the minimum tracking width of a window, in pixels.   </summary>
        /// <returns>The minimum tracking width of a window.</returns>
        public double MinimumWindowTrackWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the minimum tracking height of a window, in pixels.   </summary>
        /// <returns>The minimun tracking height of a window.</returns>
        public double MinimumWindowTrackHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the screen width, in pixels, of the primary display monitor.   </summary>
        /// <returns>The width of the screen.</returns>
        public double PrimaryScreenWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the screen height, in pixels, of the primary display monitor.   </summary>
        /// <returns>The height of the screen.</returns>
        public double PrimaryScreenHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of a button in the title bar of a window.  </summary>
        /// <returns>The width of a caption button.</returns>
        public double WindowCaptionButtonWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a button in the title bar of a window.  </summary>
        /// <returns>The height of a caption button.</returns>
        public double WindowCaptionButtonHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height (thickness), in pixels, of the horizontal sizing border around the perimeter of a window that can be resized.   </summary>
        /// <returns>The height of the border.</returns>
        public double ResizeFrameHorizontalBorderHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width (thickness), in pixels, of the vertical sizing border around the perimeter of a window that can be resized.   </summary>
        /// <returns>The width of the border.</returns>
        public double ResizeFrameVerticalBorderWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the recommended width, in pixels, of a small icon. </summary>
        /// <returns>The width of the icon.</returns>
        public double SmallIconWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the recommended height, in pixels, of a small icon. </summary>
        /// <returns>The icon height.</returns>
        public double SmallIconHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of small caption buttons.  </summary>
        /// <returns>The width of the caption button.</returns>
        public double SmallWindowCaptionButtonWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of small caption buttons.  </summary>
        /// <returns>The height of the caption button.</returns>
        public double SmallWindowCaptionButtonHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of the virtual screen.   </summary>
        /// <returns>The width of the virtual screen.</returns>
        public double VirtualScreenWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of the virtual screen.   </summary>
        /// <returns>The height of the virtual screen.</returns>
        public double VirtualScreenHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the width, in pixels, of a vertical scroll bar.  </summary>
        /// <returns>The width of a scroll bar.</returns>
        public double VerticalScrollBarWidth { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of the arrow bitmap on a vertical scroll bar.  </summary>
        /// <returns>The height of a bitmap.</returns>
        public double VerticalScrollBarButtonHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a caption area.  </summary>
        /// <returns>The height of a caption area.</returns>
        public double WindowCaptionHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of the kanji window at the bottom of the screen for systems that use double-byte characters.  </summary>
        /// <returns>The window height.</returns>
        public double KanjiWindowHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of a single-line menu bar.  </summary>
        /// <returns>The height of the menu bar.</returns>
        public double MenuBarHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the height, in pixels, of the thumb in a vertical scroll bar.  </summary>
        /// <returns>The height of the thumb.</returns>
        public double VerticalScrollBarThumbHeight { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the system is ready to use a Unicode-based Input Method Editor (IME) on a Unicode application.  </summary>
        /// <returns>
        /// <see langword="true" /> if the Input Method Manager/Input Method Editor features are enabled; otherwise, <see langword="false" />.<see langword="" /></returns>
        public bool IsImmEnabled { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the current operating system is the Microsoft Windows XP Media Center Edition. </summary>
        /// <returns>
        /// <see langword="true" /> if the current operating system is Windows XP Media Center Edition; otherwise, <see langword="false" />.</returns>
        public bool IsMediaCenter { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether drop-down menus are right-aligned with the corresponding menu item. </summary>
        /// <returns>
        /// <see langword="true" /> if drop-down menus are right-aligned; otherwise, <see langword="false" />.</returns>
        public bool IsMenuDropRightAligned { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the system is enabled for Hebrew and Arabic languages. </summary>
        /// <returns>
        /// <see langword="true" /> if the system is enabled for Hebrew and Arabic languages; otherwise, <see langword="false" />.</returns>
        public bool IsMiddleEastEnabled { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether a mouse is installed. </summary>
        /// <returns>
        /// <see langword="true" /> if a mouse is installed; otherwise, <see langword="false" />.</returns>
        public bool IsMousePresent { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the installed mouse has a vertical scroll wheel. </summary>
        /// <returns>
        /// <see langword="true" /> if the installed mouse has a vertical scroll wheel; otherwise, <see langword="false" />.</returns>
        public bool IsMouseWheelPresent { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether Microsoft Windows for Pen Computing extensions are installed. </summary>
        /// <returns>
        /// <see langword="true" /> if Pen Computing extensions are installed; otherwise, <see langword="false" />. </returns>
        public bool IsPenWindows { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the current session is remotely controlled. </summary>
        /// <returns>
        /// <see langword="true" /> if the current session is remotely controlled; otherwise, <see langword="false" />.</returns>
        public bool IsRemotelyControlled { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the calling process is associated with a Terminal Services client session. </summary>
        /// <returns>
        /// <see langword="true" /> if the calling process is associated with a Terminal Services client session; <see langword="false" /> if the calling process is associated with the Terminal Server console session.</returns>
        public bool IsRemoteSession { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the user requires information in visual format. </summary>
        /// <returns>
        /// <see langword="true" /> if the user requires an application to present information visually where it typically presents the information only in audible form; otherwise <see langword="false" />.</returns>
        public bool ShowSounds { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the computer has a low-end (slow) processor. </summary>
        /// <returns>
        /// <see langword="true" /> if the computer has a low-end (slow) processor; otherwise, <see langword="false" />.</returns>
        public bool IsSlowMachine { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the functionality of the left and right mouse buttons are swapped.  </summary>
        /// <returns>
        /// <see langword="true" /> if the functionality of the left and right mouse buttons are swapped; otherwise <see langword="false" />.</returns>
        public bool SwapButtons { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates whether the current operating system is Microsoft Windows XP Tablet PC Edition. </summary>
        /// <returns>
        /// <see langword="true" /> if the current operating system is Windows XP Tablet PC Edition; otherwise, <see langword="false" />.</returns>
        public bool IsTabletPC { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the coordinate for the left side of the virtual screen.   </summary>
        /// <returns>A screen coordinate, in pixels.</returns>
        public double VirtualScreenLeft { [ SecurityCritical ] get; set; }

        /// <summary>Gets a value that indicates the upper coordinate of the virtual screen. </summary>
        /// <returns>A screen coordinate, in pixels.</returns>
        public double VirtualScreenTop { [ SecurityCritical ] get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ThinHorizontalBorderHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ThinHorizontalBorderHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ThinVerticalBorderWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ThinVerticalBorderWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.CursorWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey CursorWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.CursorHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey CursorHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ThickHorizontalBorderHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ThickHorizontalBorderHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ThickVerticalBorderWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ThickVerticalBorderWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FixedFrameHorizontalBorderHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FixedFrameHorizontalBorderHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FixedFrameVerticalBorderWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FixedFrameVerticalBorderWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FocusHorizontalBorderHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FocusHorizontalBorderHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FocusVerticalBorderWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FocusVerticalBorderWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FullPrimaryScreenWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FullPrimaryScreenWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.FullPrimaryScreenHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey FullPrimaryScreenHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.HorizontalScrollBarButtonWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey HorizontalScrollBarButtonWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.HorizontalScrollBarHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey HorizontalScrollBarHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.HorizontalScrollBarThumbWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey HorizontalScrollBarThumbWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IconWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IconWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IconHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IconHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IconGridWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IconGridWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IconGridHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IconGridHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MaximizedPrimaryScreenWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MaximizedPrimaryScreenWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MaximizedPrimaryScreenHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MaximizedPrimaryScreenHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MaximumWindowTrackWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MaximumWindowTrackWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MaximumWindowTrackHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MaximumWindowTrackHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuCheckmarkWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuCheckmarkWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuCheckmarkHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuCheckmarkHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuButtonWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuButtonWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuButtonHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuButtonHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimumWindowWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimumWindowWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimumWindowHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimumWindowHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimizedWindowWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimizedWindowWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimizedWindowHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimizedWindowHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimizedGridWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimizedGridWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimizedGridHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimizedGridHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimumWindowTrackWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimumWindowTrackWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MinimumWindowTrackHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MinimumWindowTrackHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.PrimaryScreenWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey PrimaryScreenWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.PrimaryScreenHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey PrimaryScreenHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.WindowCaptionButtonWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey WindowCaptionButtonWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.WindowCaptionButtonHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey WindowCaptionButtonHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ResizeFrameHorizontalBorderHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ResizeFrameHorizontalBorderHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ResizeFrameVerticalBorderWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ResizeFrameVerticalBorderWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SmallIconWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SmallIconWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SmallIconHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SmallIconHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SmallWindowCaptionButtonWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SmallWindowCaptionButtonWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SmallWindowCaptionButtonHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SmallWindowCaptionButtonHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.VirtualScreenWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey VirtualScreenWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.VirtualScreenHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey VirtualScreenHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.VerticalScrollBarWidth" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey VerticalScrollBarWidthKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.VerticalScrollBarButtonHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey VerticalScrollBarButtonHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.WindowCaptionHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey WindowCaptionHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.KanjiWindowHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey KanjiWindowHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.MenuBarHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey MenuBarHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SmallCaptionHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SmallCaptionHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.VerticalScrollBarThumbHeight" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey VerticalScrollBarThumbHeightKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsImmEnabled" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsImmEnabledKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsMediaCenter" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsMediaCenterKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsMenuDropRightAligned" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsMenuDropRightAlignedKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsMiddleEastEnabled" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsMiddleEastEnabledKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsMousePresent" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsMousePresentKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsMouseWheelPresent" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsMouseWheelPresentKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsPenWindows" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsPenWindowsKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsRemotelyControlled" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsRemotelyControlledKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsRemoteSession" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsRemoteSessionKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.ShowSounds" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey ShowSoundsKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsSlowMachine" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsSlowMachineKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.SwapButtons" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey SwapButtonsKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.IsTabletPC" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey IsTabletPCKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.VirtualScreenLeft" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey VirtualScreenLeftKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.VirtualScreenTop" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey VirtualScreenTopKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see langword="FocusVisualStyle" /> property. </summary>
        /// <returns>The resource key.</returns>
        public ResourceKey FocusVisualStyleKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.NavigationChromeStyleKey" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey NavigationChromeStyleKey { get; set; }

        /// <summary>Gets the <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.NavigationChromeDownLevelStyleKey" /> property. </summary>
        /// <returns>A resource key.</returns>
        public ResourceKey NavigationChromeDownLevelStyleKey { get; set; }

        /// <summary>Gets a value indicating whether the system power is online, or that the system power status is unknown.</summary>
        /// <returns>A value in the enumeration.</returns>
        public PowerLineStatus PowerLineStatus { [ SecurityCritical ] get; set; }

        /// <summary>Gets a <see cref="T:System.Windows.ResourceKey" /> for the <see cref="P:System.Windows.SystemParameters.PowerLineStatus" /> property.</summary>
        /// <returns>A resource key.</returns>
        public ResourceKey PowerLineStatusKey { get; set; }

        /// <summary>Gets a value that indicates whether glass window frames are being used.</summary>
        /// <returns>
        /// <see langword="true" /> if glass window frames are being used; otherwise, <see langword="false" />.</returns>
        public bool IsGlassEnabled { [ SecurityCritical ] get; set; }

        /// <summary>Gets the theme name.</summary>
        /// <returns>The theme name.</returns>
        public string UxThemeName { [ SecurityCritical ] get; set; }

        /// <summary>Gets the color theme name.</summary>
        /// <returns>The color theme name.</returns>
        public string UxThemeColor { [ SecurityCritical ] get; set; }

        /// <summary>Gets the radius of the corners for a window.</summary>
        /// <returns>The degree to which the corners of a window are rounded.</returns>
        public CornerRadius WindowCornerRadius { [ SecurityCritical ] get; set; }

        /// <summary>Gets the color that is used to paint the glass window frame.</summary>
        /// <returns>The color that is used to paint the glass window frame.</returns>
        public Color WindowGlassColor { [ SecurityCritical ] get; set; }

        /// <summary>Gets the brush that paints the glass window frame.</summary>
        /// <returns>The brush that paints the glass window frame.</returns>
        public Brush WindowGlassBrush { [ SecurityCritical ] get; set; }

        /// <summary>Gets the size of the resizing border around the window.</summary>
        /// <returns>The size of the resizing border around the window, in device-independent units (1/96th of an inch).</returns>
        public Thickness WindowResizeBorderThickness { [ SecurityCritical ] get; set; }

        /// <summary>Gets the size of the non-client area of the window.</summary>
        /// <returns>The size of the non-client area of the window, in device-independent units (1/96th of an inch).</returns>
        public Thickness WindowNonClientFrameThickness { [ SecurityCritical ] get; set; }
    }
}