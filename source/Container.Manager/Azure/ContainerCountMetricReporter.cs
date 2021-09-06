using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SharpLab.Container.Manager.Azure {
    public class ContainerCountMetricReporter : BackgroundService {
        private static readonly MetricIdentifier ContainerCountMetric = new("Custom Metrics", "Container Count");

        private static readonly ContainersListParameters RunningOnlyListParameters = new() {
            Filters = new Dictionary<string, IDictionary<string, bool>> {
                { "status", new Dictionary<string, bool> { { "running", true } } }
            }
        };

        private readonly DockerClient _dockerClient;
        private readonly TelemetryClient _telemetryClient;
        private readonly ILogger<ContainerCountMetricReporter> _logger;

        public ContainerCountMetricReporter(
            DockerClient dockerClient,
            TelemetryClient telemetryClient,
            ILogger<ContainerCountMetricReporter> logger
        ) {
            _dockerClient = dockerClient;
            _telemetryClient = telemetryClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                try {
                    var containers = await _dockerClient.Containers.ListContainersAsync(RunningOnlyListParameters);

                    _telemetryClient.GetMetric(ContainerCountMetric).TrackValue(containers.Count);
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "Failed to report container count");
                    await Task.Delay(TimeSpan.FromMinutes(4), stoppingToken);
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
