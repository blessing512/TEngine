﻿using System;
using NLog;

namespace ET
{
    public class NLogger: ILog
    {
        private readonly NLog.Logger logger;

        static NLogger()
        {
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("../Configs/NLog/NLog.config");
            LogManager.Configuration.Variables["currentDir"] = Environment.CurrentDirectory;
        }

        public NLogger(string name, int process, int fiber)
        {
            string loggerName = $"{(uint)process:000000}.{(uint)fiber:0000000000}.{name}";
#if DEBUG
            loggerName = "Server";
#endif
            this.logger = LogManager.GetLogger(loggerName);
        }

        public void Trace(string message)
        {
            this.logger.Trace(message);
        }

        public void Warning(string message)
        {
            this.logger.Warn(message);
        }

        public void Info(string message)
        {
            this.logger.Info(message);
        }

        public void Debug(string message)
        {
            this.logger.Debug(message);
        }

        public void Error(string message)
        {
            this.logger.Error(message);
        }

        public void Error(Exception e)
        {
            this.logger.Error(e.ToString());
        }

#if DOTNET
        public void Trace(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Trace(message.ToStringAndClear());
        }

        public void Warning(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Warn(message.ToStringAndClear());
        }

        public void Info(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Info(message.ToStringAndClear());
        }

        public void Debug(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Debug(message.ToStringAndClear());
        }

        public void Error(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Error(message.ToStringAndClear());
        }
#endif
    }
}