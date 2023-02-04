using Serilog;
using System;
using System.Diagnostics;
using System.Reflection;

namespace project_leadconsult_core.Utils
{
    public static class SerilogLogger
    {
        /// <summary>
        /// Traces the catch.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="exception">The exception.</param>
        public static void TraceCatch(Guid correlationId, Exception exception)
        {
            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
            string unit = string.Concat(caller.ReflectedType.Name, ".", caller.Name);

            Log.Fatal("CorrelationID: {@CorrelationID}, {@u} EXCEPTION: {@parameters}", correlationId, unit, exception);
        }

        /// <summary>
        /// Traces the error.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="parameters">The parameters.</param>
        public static void TraceError(Guid correlationId, params object[] parameters)
        {
            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
            string unit = string.Concat(caller.ReflectedType.Name, ".", caller.Name);

            Log.Error("CorrelationID: {@CorrelationID}, {@u} ERROR: {@parameters}", correlationId, unit, parameters);
        }

        /// <summary>
        /// Traces the log.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="parameters">The parameters.</param>
        public static void TraceLogIn(Guid correlationId, params object[] parameters)
        {
            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
            string unit = string.Concat(caller.ReflectedType.Name, ".", caller.Name);

            Log.Information("CorrelationID: {@CorrelationID}, {@u} IN: {@parameters}", correlationId, unit, parameters);
        }

        /// <summary>
        /// Traces the log out.
        /// </summary>
        /// <param name="correlationId">The correlation identifier.</param>
        /// <param name="parameters">The parameters.</param>
        public static void TraceLogOut(Guid correlationId, params object[] parameters)
        {
            MethodBase caller = new StackTrace().GetFrame(1).GetMethod();
            string unit = string.Concat(caller.ReflectedType.Name, ".", caller.Name);

            Log.Information("CorrelationID: {@CorrelationID}, {@u} OUT: {@parameters}", correlationId, unit, parameters);
        }
    }
}
