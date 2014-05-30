namespace MDT.ServiceMonitor
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MonitorProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MonitorInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // MonitorProcessInstaller
            // 
            this.MonitorProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.MonitorProcessInstaller.Password = null;
            this.MonitorProcessInstaller.Username = null;
            // 
            // MonitorInstaller
            // 
            this.MonitorInstaller.Description = "MDT数据交换任务监控";
            this.MonitorInstaller.DisplayName = "MDT数据交换任务监控";
            this.MonitorInstaller.ServiceName = "MDTMonitor";
            this.MonitorInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MonitorProcessInstaller,
            this.MonitorInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MonitorProcessInstaller;
        private System.ServiceProcess.ServiceInstaller MonitorInstaller;
    }
}