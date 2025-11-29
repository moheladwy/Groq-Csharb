namespace Groq.Core.Configurations;

public static class ServiceTiers
{
    // Highest-performance tier for critical production workloads (enterprise-only)
    public const string Performance = "performance";

    // Default tier if `service_tier` is omitted. Standard predictable speed with
    // possible queue latency during peak times.
    public const string OnDemand = "on_demand";

    // Higher throughput on a best-effort basis; may return over-capacity errors.
    public const string Flex = "flex";

    // Automatically choose the best available tier for the request.
    public const string Auto = "auto";
}
