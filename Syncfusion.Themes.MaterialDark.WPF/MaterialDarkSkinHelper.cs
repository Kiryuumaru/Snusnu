using System.Reflection;
using Syncfusion.SfSkinManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Syncfusion.Themes.MaterialDark.WPF
{
    public class MaterialDarkSkinHelper : SkinHelper
    {
        [Obsolete("GetDictonaries is deprecated, please use GetDictionaries instead.")]
        public override List<string> GetDictonaries(string type, string style)
        {
            return GetDictionaries(type, style);
        }
        public override List<string> GetDictionaries(String type, string style)
        {
            string rootStylePath = "/Syncfusion.Themes.MaterialDark.WPF;component/";
            List<string> styles = new List<string>();
            # region Switch

			switch (type)
			{
				case "MSControls":
					styles.Add(rootStylePath + "MSControl/GlyphButton.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphToggleButton.xaml");
					styles.Add(rootStylePath + "MSControl/ToolTip.xaml");
					styles.Add(rootStylePath + "MSControl/FlatButton.xaml");
					styles.Add(rootStylePath + "MSControl/FlatPrimaryButton.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphRepeatButton.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphDropdownExpander.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphEditableDropdownExpander.xaml");
					styles.Add(rootStylePath + "MSControl/ListBox.xaml");
					styles.Add(rootStylePath + "MSControl/DatePicker.xaml");
					styles.Add(rootStylePath + "MSControl/Calendar.xaml");
					styles.Add(rootStylePath + "MSControl/ComboBox.xaml");
					styles.Add(rootStylePath + "MSControl/TextBox.xaml");
					styles.Add(rootStylePath + "MSControl/Slider.xaml");
					styles.Add(rootStylePath + "MSControl/TextBlock.xaml");
					styles.Add(rootStylePath + "MSControl/ScrollViewer.xaml");
					styles.Add(rootStylePath + "MSControl/Window.xaml");
					styles.Add(rootStylePath + "MSControl/PrimaryButton.xaml");
					styles.Add(rootStylePath + "MSControl/Button.xaml");
					styles.Add(rootStylePath + "MSControl/GroupBox.xaml");
					styles.Add(rootStylePath + "MSControl/Expander.xaml");
					styles.Add(rootStylePath + "MSControl/GridSplitter.xaml");
					styles.Add(rootStylePath + "MSControl/ToggleButton.xaml");
					styles.Add(rootStylePath + "MSControl/RepeatButton.xaml");
					styles.Add(rootStylePath + "MSControl/Menu.xaml");
					styles.Add(rootStylePath + "MSControl/Separator.xaml");
					styles.Add(rootStylePath + "MSControl/ListView.xaml");
					styles.Add(rootStylePath + "MSControl/Label.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphPrimaryToggleButton.xaml");
					styles.Add(rootStylePath + "MSControl/CheckBox.xaml");
					styles.Add(rootStylePath + "MSControl/ProgressBar.xaml");
					styles.Add(rootStylePath + "MSControl/RadioButton.xaml");
					styles.Add(rootStylePath + "MSControl/TabControl.xaml");
					styles.Add(rootStylePath + "MSControl/GlyphTreeExpander.xaml");
					styles.Add(rootStylePath + "MSControl/TreeView.xaml");
					styles.Add(rootStylePath + "MSControl/FlatToggleButton.xaml");
					styles.Add(rootStylePath + "MSControl/Hyperlink.xaml");
					styles.Add(rootStylePath + "MSControl/StatusBar.xaml");
					styles.Add(rootStylePath + "MSControl/PasswordBox.xaml");
					styles.Add(rootStylePath + "MSControl/ResizeGrip.xaml");
					styles.Add(rootStylePath + "MSControl/ToolBar.xaml");
					styles.Add(rootStylePath + "MSControl/RichTextBox.xaml");
					styles.Add(rootStylePath + "MSControl/DataGrid.xaml");
					break;
				case "SfScheduler":
					styles.Add(rootStylePath + "SfScheduler/SfScheduler.xaml");
					styles.Add(rootStylePath + "SfScheduler/AgendaViewStyle.xaml");
					styles.Add(rootStylePath + "SfScheduler/AllDayAppointmentPanelStyle.xaml");
					styles.Add(rootStylePath + "SfScheduler/AppointmentStyle.xaml");
					styles.Add(rootStylePath + "SfScheduler/MonthViewStyle.xaml");
					styles.Add(rootStylePath + "SfScheduler/SchedulerHeaderStyle.xaml");
					styles.Add(rootStylePath + "SfScheduler/TimeSlotViewStyle.xaml");
					styles.Add(rootStylePath + "SfScheduler/ViewHeaderStyle.xaml");
					break;
				case "ChromelessWindow":
					styles.Add(rootStylePath + "ChromelessWindow/ChromelessWindow.xaml");
					break;
				case "DoubleTextBox":
					styles.Add(rootStylePath + "DoubleTextBox/DoubleTextBox.xaml");
					break;
				case "PercentTextBox":
					styles.Add(rootStylePath + "PercentTextBox/PercentTextBox.xaml");
					break;
				case "CurrencyTextBox":
					styles.Add(rootStylePath + "CurrencyTextBox/CurrencyTextBox.xaml");
					break;
				case "UpDown":
					styles.Add(rootStylePath + "UpDown/UpDown.xaml");
					break;
				case "MaskedTextBox":
					styles.Add(rootStylePath + "MaskedTextBox/MaskedTextBox.xaml");
					break;
				case "ComboBoxAdv":
					styles.Add(rootStylePath + "ComboBoxAdv/ComboBoxAdv.xaml");
					break;
				case "TimeSpanEdit":
					styles.Add(rootStylePath + "TimeSpanEdit/TimeSpanEdit.xaml");
					break;
				case "GridPrintPreviewControl":
					styles.Add(rootStylePath + "GridPrintPreviewControl/GridPrintPreviewControl.xaml");
					break;
				case "SfDataGrid":
					styles.Add(rootStylePath + "SfDataGrid/SfDataGrid.xaml");
					break;
				case "SfTreeGrid":
					styles.Add(rootStylePath + "SfTreeGrid/SfTreeGrid.xaml");
					break;
				case "SfDataPager":
					styles.Add(rootStylePath + "SfDataPager/SfDataPager.xaml");
					break;
				case "SfChart":
					styles.Add(rootStylePath + "SfChart/SfChart.xaml");
					styles.Add(rootStylePath + "SfChart/ChartArea.xaml");
					styles.Add(rootStylePath + "SfChart/ChartAxis.xaml");
					styles.Add(rootStylePath + "SfChart/ChartToolBar.xaml");
					styles.Add(rootStylePath + "SfChart/SfChartCommon.xaml");
					styles.Add(rootStylePath + "SfChart/Resizer.xaml");
					break;
				case "SfChart3D":
					styles.Add(rootStylePath + "SfChart3D/SfChart3D.xaml");
					break;
				case "PrintPreview":
					styles.Add(rootStylePath + "PrintPreview/PrintPreview.xaml");
					break;
				case "PrintPreviewControl":
					styles.Add(rootStylePath + "PrintPreviewControl/PrintPreviewControl.xaml");
					break;
				case "Stencil":
					styles.Add(rootStylePath + "Stencil/Stencil.xaml");
					break;
				case "SfDiagram":
					styles.Add(rootStylePath + "SfDiagram/SfDiagram.xaml");
					break;
				case "SfCircularGauge":
					styles.Add(rootStylePath + "SfCircularGauge/SfCircularGauge.xaml");
					break;
				case "SfLinearGauge":
					styles.Add(rootStylePath + "SfLinearGauge/SfLinearGauge.xaml");
					break;
				case "SfDigitalGauge":
					styles.Add(rootStylePath + "SfDigitalGauge/SfDigitalGauge.xaml");
					break;
				case "SfKanban":
					styles.Add(rootStylePath + "SfKanban/SfKanban.xaml");
					break;
				case "SfTreeMap":
					styles.Add(rootStylePath + "SfTreeMap/SfTreeMap.xaml");
					break;
				case "SfMap":
					styles.Add(rootStylePath + "SfMap/SfMap.xaml");
					break;
				case "SfSmithChart":
					styles.Add(rootStylePath + "SfSmithChart/SfSmithChart.xaml");
					break;
				case "SfSunburstChart":
					styles.Add(rootStylePath + "SfSunburstChart/SfSunburstChart.xaml");
					break;
				case "SfBulletGraph":
					styles.Add(rootStylePath + "SfBulletGraph/SfBulletGraph.xaml");
					break;
				case "SfDateTimeRangeNavigator":
					styles.Add(rootStylePath + "SfDateTimeRangeNavigator/SfDateTimeRangeNavigator.xaml");
					break;
				case "SfAreaSparkline":
					styles.Add(rootStylePath + "SfAreaSparkline/SfAreaSparkline.xaml");
					break;
				case "SfLineSparkline":
					styles.Add(rootStylePath + "SfLineSparkline/SfLineSparkline.xaml");
					break;
				case "SfColumnSparkline":
					styles.Add(rootStylePath + "SfColumnSparkline/SfColumnSparkline.xaml");
					break;
				case "SfWinLossSparkline":
					styles.Add(rootStylePath + "SfWinLossSparkline/SfWinLossSparkline.xaml");
					break;
				case "SfHeatMap":
					styles.Add(rootStylePath + "SfHeatMap/SfHeatMap.xaml");
					break;
				case "ColorPicker":
					styles.Add(rootStylePath + "ColorPicker/ColorPicker.xaml");
					break;
				case "ColorEdit":
					styles.Add(rootStylePath + "ColorEdit/ColorEdit.xaml");
					break;
				case "IntegerTextBox":
					styles.Add(rootStylePath + "IntegerTextBox/IntegerTextBox.xaml");
					break;
				case "PropertyGrid":
					styles.Add(rootStylePath + "PropertyGrid/PropertyGrid.xaml");
					break;
				case "SfMaskedEdit":
					styles.Add(rootStylePath + "SfMaskedEdit/SfMaskedEdit.xaml");
					break;
				case "SfColorPalette":
					styles.Add(rootStylePath + "SfColorPalette/SfColorPalette.xaml");
					break;
				case "ColorPickerPalette":
					styles.Add(rootStylePath + "ColorPickerPalette/ColorPickerPalette.xaml");
					break;
				case "SfCalculator":
					styles.Add(rootStylePath + "SfCalculator/SfCalculator.xaml");
					break;
				case "Clock":
					styles.Add(rootStylePath + "Clock/Clock.xaml");
					break;
				case "SfDomainUpDown":
					styles.Add(rootStylePath + "SfDomainUpDown/SfDomainUpDown.xaml");
					break;
				case "SfDateSelector":
					styles.Add(rootStylePath + "SfDateSelector/SfDateSelector.xaml");
					break;
				case "SfTextBoxExt":
					styles.Add(rootStylePath + "SfTextBoxExt/SfTextBoxExt.xaml");
					break;
				case "SfDatePicker":
					styles.Add(rootStylePath + "SfDatePicker/SfDatePicker.xaml");
					break;
				case "SfTimeSelector":
					styles.Add(rootStylePath + "SfTimeSelector/SfTimeSelector.xaml");
					break;
				case "SfTimePicker":
					styles.Add(rootStylePath + "SfTimePicker/SfTimePicker.xaml");
					break;
				case "SfTextInputLayout":
					styles.Add(rootStylePath + "SfTextInputLayout/SfTextInputLayout.xaml");
					break;
				case "SfRadialSlider":
					styles.Add(rootStylePath + "SfRadialSlider/SfRadialSlider.xaml");
					break;
				case "SfRangeSlider":
					styles.Add(rootStylePath + "SfRangeSlider/SfRangeSlider.xaml");
					break;
				case "SfRating":
					styles.Add(rootStylePath + "SfRating/SfRating.xaml");
					break;
				case "PinnableListBox":
					styles.Add(rootStylePath + "PinnableListBox/PinnableListBox.xaml");
					break;
				case "CalendarEdit":
					styles.Add(rootStylePath + "CalendarEdit/CalendarEdit.xaml");
					break;
				case "ButtonAdv":
					styles.Add(rootStylePath + "ButtonAdv/ButtonAdv.xaml");
					break;
				case "DropDownButtonAdv":
					styles.Add(rootStylePath + "DropDownButtonAdv/DropDownButtonAdv.xaml");
					break;
				case "SplitButtonAdv":
					styles.Add(rootStylePath + "SplitButtonAdv/SplitButtonAdv.xaml");
					break;
				case "SfMultiColumnDropDownControl":
					styles.Add(rootStylePath + "SfMultiColumnDropDownControl/SfMultiColumnDropDownControl.xaml");
					break;
				case "CheckListBox":
					styles.Add(rootStylePath + "CheckListBox/CheckListBox.xaml");
					break;
				case "FontListBox":
					styles.Add(rootStylePath + "FontListBox/FontListBox.xaml");
					break;
				case "FontListComboBox":
					styles.Add(rootStylePath + "FontListComboBox/FontListComboBox.xaml");
					break;
				case "SfBusyIndicator":
					styles.Add(rootStylePath + "SfBusyIndicator/SfBusyIndicator.xaml");
					break;
				case "SfLinearProgressBar":
					styles.Add(rootStylePath + "SfLinearProgressBar/SfLinearProgressBar.xaml");
					break;
				case "SfCircularProgressBar":
					styles.Add(rootStylePath + "SfCircularProgressBar/SfCircularProgressBar.xaml");
					break;
				case "SfStepProgressBar":
					styles.Add(rootStylePath + "SfStepProgressBar/SfStepProgressBar.xaml");
					break;
				case "SfHubTile":
					styles.Add(rootStylePath + "SfHubTile/SfHubTile.xaml");
					break;
				case "SfPulsingTile":
					styles.Add(rootStylePath + "SfPulsingTile/SfPulsingTile.xaml");
					break;
				case "BusyIndicator":
					styles.Add(rootStylePath + "BusyIndicator/BusyIndicator.xaml");
					break;
				case "NotifyIcon":
					styles.Add(rootStylePath + "NotifyIcon/NotifyIcon.xaml");
					break;
				case "DocumentContainer":
					styles.Add(rootStylePath + "DocumentContainer/DocumentContainer.xaml");
					break;
				case "TabControlExt":
					styles.Add(rootStylePath + "TabControlExt/TabControlExt.xaml");
					break;
				case "Gallery":
					styles.Add(rootStylePath + "Gallery/Gallery.xaml");
					break;
				case "DockingManager":
					styles.Add(rootStylePath + "DockingManager/DockingManager.xaml");
					break;
				case "RibbonWindow":
					styles.Add(rootStylePath + "Ribbon/RibbonWindow.xaml");
					break;
				case "TreeViewAdv":
					styles.Add(rootStylePath + "TreeViewAdv/TreeViewAdv.xaml");
					break;
				case "Ribbon":
					styles.Add(rootStylePath + "Ribbon/Ribbon.xaml");
					styles.Add(rootStylePath + "Ribbon/QATCustomizationDialog.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonBackstage.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonGallery.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonBar.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonButton.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonContextMenu.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonItems.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonTab.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonToggleButton.xaml");
					styles.Add(rootStylePath + "Ribbon/RibbonDropDownButton.xaml");
					break;
				case "TabSplitter":
					styles.Add(rootStylePath + "TabSplitter/TabSplitter.xaml");
					break;
				case "SfGridSplitter":
					styles.Add(rootStylePath + "SfGridSplitter/SfGridSplitter.xaml");
					break;
				case "HierarchyNavigator":
					styles.Add(rootStylePath + "HierarchyNavigator/HierarchyNavigator.xaml");
					break;
				case "SfAccordion":
					styles.Add(rootStylePath + "SfAccordion/SfAccordion.xaml");
					break;
				case "SfTreeView":
					styles.Add(rootStylePath + "SfTreeView/SfTreeView.xaml");
					break;
				case "WizardControl":
					styles.Add(rootStylePath + "WizardControl/WizardControl.xaml");
					break;
				case "TaskBar":
					styles.Add(rootStylePath + "TaskBar/TaskBar.xaml");
					break;
				case "ToolBarAdv":
					styles.Add(rootStylePath + "ToolBarAdv/ToolBarAdv.xaml");
					styles.Add(rootStylePath + "ToolBarAdv/ToolBarResources.xaml");
					break;
				case "SfRadialMenu":
					styles.Add(rootStylePath + "SfRadialMenu/SfRadialMenu.xaml");
					break;
				case "TabNavigationControl":
					styles.Add(rootStylePath + "TabNavigationControl/TabNavigationControl.xaml");
					break;
				case "MenuAdv":
					styles.Add(rootStylePath + "MenuAdv/MenuAdv.xaml");
					break;
				case "SfTreeNavigator":
					styles.Add(rootStylePath + "SfTreeNavigator/SfTreeNavigator.xaml");
					break;
				case "GroupBar":
					styles.Add(rootStylePath + "GroupBar/GroupBar.xaml");
					break;
				case "TileViewControl":
					styles.Add(rootStylePath + "TileViewControl/TileViewControl.xaml");
					break;
				case "CardView":
					styles.Add(rootStylePath + "CardView/CardView.xaml");
					break;
				case "PdfViewerControl":
					styles.Add(rootStylePath + "PdfViewerControl/PdfViewerControl.xaml");
					break;
				case "FontDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "ParagraphDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "HyperlinkDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "PasswordDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "InsertTableDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "FindAndReplaceDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "ListDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "ShowMessageDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "TableDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "CellOptionsDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "TableOptionsDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "BordersAndShadingDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "StylesDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "StyleDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "MiniToolBar":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "ContextMenu":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "BulletsAndNumberingDialog":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					break;
				case "SfRichTextRibbon":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextRibbon.xaml");
					break;
				case "SfRichTextBoxAdv":
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxAdv.xaml");
					styles.Add(rootStylePath + "SfRichTextBoxAdv/ContextMenu.xaml");
					styles.Add(rootStylePath + "SfRichTextBoxAdv/Dialogs.xaml");
					styles.Add(rootStylePath + "SfRichTextBoxAdv/FormatDialogs.xaml");
					styles.Add(rootStylePath + "SfRichTextBoxAdv/MiniToolBar.xaml");
					styles.Add(rootStylePath + "SfRichTextBoxAdv/SfRichTextBoxCommon.xaml");
					styles.Add(rootStylePath + "SfRichTextBoxAdv/StyleDialogs.xaml");
					styles.Add(rootStylePath + "SfRichTextBoxAdv/TableDialogs.xaml");
					break;
				case "FindReplaceControl":
					styles.Add(rootStylePath + "EditControl/EditControl.xaml");
					break;
				case "EditControl":
					styles.Add(rootStylePath + "EditControl/EditControl.xaml");
					break;
				case "AutoComplete":
					styles.Add(rootStylePath + "AutoComplete/AutoComplete.xaml");
					break;
				case "DateTimeEdit":
					styles.Add(rootStylePath + "DateTimeEdit/DateTimeEdit.xaml");
					break;
				case "SfImageEditor":
					styles.Add(rootStylePath + "SfImageEditor/SfImageEditor.xaml");
					break;
				case "Common":
					styles.Add(rootStylePath + "Common/Common.xaml");
					break;
				case "Brushes":
					styles.Add(rootStylePath + "Common/Brushes.xaml");
					break;
			}

            # endregion

            return styles;
        }
    }

    /// <summary>
    /// Represents a class that holds the respective theme color and common key values for customization
    /// </summary>
    public class MaterialDarkThemeSettings: IThemeSetting
    {
        /// <summary>
        /// Constructor to create an instance of MaterialDarkThemeSettings.
        /// </summary>
        public MaterialDarkThemeSettings()
        {
            #region Initialize Value 
			HeaderFontSize = 16;
			SubHeaderFontSize = 14;
			TitleFontSize = 14;
			SubTitleFontSize = 12;
			BodyFontSize = 12;
			BodyAltFontSize = 10;

            #endregion
        }

        #region Properties


		/// <summary>
		/// Gets or sets the font size of header related areas of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.HeaderFontSize = 16;
		/// ]]>
		/// </code>
		/// </example>
		public Double HeaderFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font size of sub header related areas of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.SubHeaderFontSize = 14;
		/// ]]>
		/// </code>
		/// </example>
		public Double SubHeaderFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font size of title related areas of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.TitleFontSize = 14;
		/// ]]>
		/// </code>
		/// </example>
		public Double TitleFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font size of sub title related areas of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.SubTitleFontSize = 12;
		/// ]]>
		/// </code>
		/// </example>
		public Double SubTitleFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font size of content area of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.BodyFontSize = 12;
		/// ]]>
		/// </code>
		/// </example>
		public Double BodyFontSize { get; set; }


		/// <summary>
		/// Gets or sets the alternate font size of content area of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.Body AltFontSize = 10;
		/// ]]>
		/// </code>
		/// </example>
		public Double BodyAltFontSize { get; set; }


		/// <summary>
		/// Gets or sets the font family of text in control for selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.FontFamily = new FontFamily("Callibri");
		/// ]]>
		/// </code>
		/// </example>
		public FontFamily FontFamily { get; set; }

		private Brush primarybackground;


		/// <summary>
		/// Gets or sets the primary background color of content area of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.PrimaryBackground = Brushes.Red;
		/// ]]>
		/// </code>
		/// </example>
		public Brush PrimaryBackground
		{
			get
			{
				return primarybackground;
			}
			set
			{
				primarybackground = value;
				PrimaryBackgroundOpacity = ThemeSettingsHelper.GetDerivationColor(value, 0, 0.25);
				PrimaryBackgroundOpacity2 = ThemeSettingsHelper.GetDerivationColor(value, 0, 0.3);
				PrimaryBackgroundOpacity3 = ThemeSettingsHelper.GetDerivationColor(value, 0, 0.4);
				PrimaryColorForeground = value;
				PrimaryDarkest = ThemeSettingsHelper.GetDerivationColor(value, 0.2, 0);
				PrimaryDarken = ThemeSettingsHelper.GetDerivationColor(value, 0.07, 0);
				PrimaryDark = ThemeSettingsHelper.GetDerivationColor(value, 0.12, 0);
				PrimaryLight = ThemeSettingsHelper.GetDerivationColor(value, 0.07, 0);
				PrimaryLighten = ThemeSettingsHelper.GetDerivationColor(value, 0.2, 0);
				PrimaryLightest = ThemeSettingsHelper.GetDerivationColor(value, 0.14, 0);
			}
		}


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryBackgroundOpacity { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryBackgroundOpacity2 { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryBackgroundOpacity3 { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryColorForeground { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryDarkest { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryDarken { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryDark { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryLight { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryLighten { get; set; }


		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		[System.ComponentModel.BrowsableAttribute(false)]
		public Brush PrimaryLightest { get; set; }

		private Brush primaryforeground;


		/// <summary>
		/// Gets or sets the primary foreground color of content area of control in selected theme
		/// </summary>
		/// <example>
		/// <code language="C#">
		/// <![CDATA[
		/// MaterialDarkThemeSettings materialDarkThemeSettings = new MaterialDarkThemeSettings();
		/// materialDarkThemeSettings.PrimaryForeground = Brushes.AntiqueWhite;
		/// ]]>
		/// </code>
		/// </example>
		public Brush PrimaryForeground
		{
			get
			{
				return primaryforeground;
			}
			set
			{
				primaryforeground = value;
			}
		}

        #endregion

        /// <summary>
        /// Helper method to decide on display property name using property mappings 
        /// </summary>
        /// <returns>Dictionary of property mappings</returns>
        /// <exclude/>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.Browsable(false)]
        public Dictionary<string, string> GetPropertyMappings()
        {
            Dictionary<string, string> propertyMappings = new Dictionary<string, string>();
            #region PropertyMappings
			propertyMappings.Add("HeaderFontSize", "HeaderTextStyle");
			propertyMappings.Add("SubHeaderFontSize", "SubHeaderTextStyle");
			propertyMappings.Add("TitleFontSize", "TitleTextStyle");
			propertyMappings.Add("SubTitleFontSize", "SubTitleTextStyle");
			propertyMappings.Add("BodyFontSize", "BodyTextStyle");
			propertyMappings.Add("BodyAltFontSize", "CaptionText");
			propertyMappings.Add("FontFamily", "ThemeFontFamily");

            #endregion
            return propertyMappings;
        }
    }
}
