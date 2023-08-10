using Telerik.Maui.Controls.Compatibility;
using TelerikEmbeddingSample.Business.Services;

namespace TelerikEmbeddingSample;

public class App : Application
{
	protected Window? MainWindow { get; private set; }
	protected IHost? Host { get; private set; }

	protected async override void OnLaunched(LaunchActivatedEventArgs args)
	{
		var builder = this.CreateBuilder(args)
			// Add navigation support for toolkit controls such as TabBar and NavigationView
			.UseToolkitNavigation()
			.Configure(host => host
#if DEBUG
				// Switch to Development environment when running in DEBUG
				.UseEnvironment(Environments.Development)
#endif
				.UseLogging(configure: (context, logBuilder) =>
				{
					// Configure log levels for different categories of logging
					logBuilder
						.SetMinimumLevel(
							context.HostingEnvironment.IsDevelopment() ?
								LogLevel.Information :
								LogLevel.Warning)

						// Default filters for core Uno Platform namespaces
						.CoreLogLevel(LogLevel.Warning);
				}, enableUnoLogging: true)
				.UseSerilog(consoleLoggingEnabled: true, fileLoggingEnabled: true)
				.UseConfiguration(configure: configBuilder =>
					configBuilder
						.EmbeddedSource<App>()
						.Section<AppConfig>()
				)
				// Enable localization (see appsettings.json for supported languages)
				.UseLocalization()
				.UseMauiEmbedding(this, maui => maui.UseMauiControls()
					.UseMauiEmbeddingResources<MauiControls.EmbeddedResources>())
				.ConfigureServices((context, services) => {
					services.AddSingleton<DataGenerator>()
						.AddSingleton<IResourceService, AssemblyResourceService>()
						.AddSingleton<ISerializationService, SerializationService>();
				})
				.UseNavigation(RegisterRoutes)
			);
		MainWindow = builder.Window;

		Host = await builder.NavigateAsync<Shell>();
	}

	private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
	{
		views.Register(
			new ViewMap(ViewModel: typeof(ShellViewModel)),
			new ViewMap<MainPage, MainViewModel>(),
			new ViewMap<AccordionControlPage, AccordionControlViewModel>(),
			new ViewMap<BadgeViewControlPage, BadgeViewControlViewModel>(),
			new ViewMap<CalendarControlPage, CalendarControlViewModel>(),
			new ViewMap<DataGridControlPage, DataGridControlViewModel>(),
			new ViewMap<AreaChartControlPage, AreaChartControlViewModel>(),
			new ViewMap<FinancialChartControlPage, FinancialChartControlViewModel>(),
			new ViewMap<GuageControlPage>(),
			new ViewMap<PdfViewerControlPage, PdfViewerControlViewModel>()
		);

		routes.Register(
			new RouteMap("", View: views.FindByViewModel<ShellViewModel>(),
				Nested: new RouteMap[]
				{
					new RouteMap("Main", View: views.FindByViewModel<MainViewModel>(), Nested: new RouteMap[]
					{
						new RouteMap("Accordion", View: views.FindByViewModel<AccordionControlViewModel>(), IsDefault: true),
						new RouteMap("AreaChart"),
						new RouteMap("BadgeView", View: views.FindByViewModel<BadgeViewControlViewModel>()),
						new RouteMap("Calendar", View: views.FindByViewModel<CalendarControlViewModel>()),
						new RouteMap("DataGrid", View: views.FindByViewModel<DataGridControlViewModel>()),
						new RouteMap("FinancialChart"),
						new RouteMap("Gauge", View: views.FindByView<GuageControlPage>()),
						new RouteMap("PdfViewer", View: views.FindByViewModel<PdfViewerControlViewModel>()),
					}),
				}
			)
		);
	}
}
