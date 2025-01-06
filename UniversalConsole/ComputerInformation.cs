using Hardware.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UniversalConsoleShared;

namespace UniversalConsole
{
    internal class ComputerInformation
    {
        Counter counter;
        Dictionary<string, Dictionary<string, List<string>>> content;
        HardwareInfo hardwareInfo;
        public ComputerInformation()
        {
            counter = new Counter();
            content = new Dictionary<string, Dictionary<string, List<string>>>();
            hardwareInfo = new HardwareInfo();
        }

        public bool Retrieve(bool writeToFile, string? fileLocation)
        {
            counter.Count = 1;

            Task heavyTask = Task.Run(() => 
            {
                try
                {
                    hardwareInfo.RefreshAll();

                }
                catch(Exception e)
                {
                    ConsoleAlert.ErrorInternal();
                    ErrorLog.Write(e.Message, DateTime.Now);
                }

            });

            try
            {
                // Display the temporary message at the bottom of the console
                int originalCursorTop = Console.CursorTop;
                int originalCursorLeft = Console.CursorLeft;

                Task.Run(() =>
                {
                    while (!heavyTask.IsCompleted)
                    {
                        int bottom = Console.WindowTop + Console.WindowHeight - 1;
                        Console.SetCursorPosition(0, bottom);
                        Console.Write("Please wait... Heavy function is running...");
                        Thread.Sleep(500);
                        // Clear the line for the next update
                        Console.SetCursorPosition(0, bottom);
                        Console.Write(new string(' ', Console.WindowWidth));
                    }
                }).Wait(); // Wait for the heavy task to complete

                // Restore the cursor position
                Console.SetCursorPosition(originalCursorLeft, originalCursorTop);



                Console.WriteLine("Hardware Information:\n\n");

                Console.WriteLine("*****************************");

                #region Battery
                // Battery List
                string batteryKey = "Battery List:";
                Console.WriteLine(batteryKey);
                Dictionary<string, List<string>> batteries = new Dictionary<string, List<string>>();
                foreach (var battery in hardwareInfo.BatteryList)
                {
                    List<string> batteryCharacteristics = new List<string>();
                    batteryCharacteristics.Add($"    BatteryStatus: {battery.BatteryStatus}");
                    batteryCharacteristics.Add($"    BatteryStatusDescription: {battery.BatteryStatusDescription}");
                    batteryCharacteristics.Add($"    DesignCapacity: {battery.DesignCapacity}");
                    batteryCharacteristics.Add($"    EstimatedChargeRemaining: {battery.EstimatedChargeRemaining}");
                    batteryCharacteristics.Add($"    EstimatedRunTime: {battery.EstimatedRunTime}");
                    batteryCharacteristics.Add($"    TimeOnBattery: {battery.TimeOnBattery}");
                    batteryCharacteristics.Add($"    FullChargeCapacity: {battery.FullChargeCapacity}");
                    batteryCharacteristics.Add($"    MaxRechargeTime: {battery.MaxRechargeTime}");
                    batteryCharacteristics.Add($"    TimeToFullCharge: {battery.TimeToFullCharge}");
                    batteryCharacteristics.Add($"    ExpectedLife: {battery.ExpectedLife}");
                    batteries[$"- Battery #{counter.Count}:"] = batteryCharacteristics;
                    counter.Count++;
                }

                foreach (var battery in batteries.Keys)
                {
                    Console.WriteLine(battery);
                    foreach (string charac in batteries[battery])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[batteryKey] = batteries;

                counter.Count = 0;
                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region BIOS

                // BIOS List
                string biosKey = "BIOS List:";
                Console.WriteLine(biosKey);
                Dictionary<string, List<string>> bioses = new Dictionary<string, List<string>>();
                counter.Count = 1;
                foreach (var bios in hardwareInfo.BiosList)
                {
                    List<string> biosCharacteristics = new List<string>();
                    biosCharacteristics.Add($"    Name: {bios.Name}");
                    biosCharacteristics.Add($"    Version: {bios.Version}");
                    biosCharacteristics.Add($"    Description: {bios.Description}");
                    biosCharacteristics.Add($"    Manufacturer: {bios.Manufacturer}");
                    biosCharacteristics.Add($"    ReleaseDate: {bios.ReleaseDate}");
                    biosCharacteristics.Add($"    SerialNumber: {bios.SerialNumber}");
                    biosCharacteristics.Add($"    Caption: {bios.Caption}");
                    biosCharacteristics.Add($"    SoftwareElementID: {bios.SoftwareElementID}");
                    bioses[$"- BIOS #{counter.Count}:"] = biosCharacteristics;
                    counter.Count++;
                }

                // Print BIOS characteristics
                foreach (var bios in bioses.Keys)
                {
                    Console.WriteLine(bios);
                    foreach (string charac in bioses[bios])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[biosKey] = bioses;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region CPU
                // CPU List
                string cpuKey = "CPU List:";
                Console.WriteLine(cpuKey);
                Dictionary<string, List<string>> cpus = new Dictionary<string, List<string>>();
                counter.Count = 1;

                foreach (var cpu in hardwareInfo.CpuList)
                {
                    List<string> cpuCharacteristics = new List<string>();
                    cpuCharacteristics.Add($"    Name: {cpu.Name}");
                    cpuCharacteristics.Add($"    Caption: {cpu.Caption}");
                    cpuCharacteristics.Add($"    Description: {cpu.Description}");
                    cpuCharacteristics.Add($"    Manufacturer: {cpu.Manufacturer}");

                    // Generate a string for CPU Core details
                    StringBuilder coreDetails = new StringBuilder();
                    int count = 1;
                    foreach (CpuCore cc in cpu.CpuCoreList)
                    {
                        coreDetails.AppendLine($"        ---{count}---");
                        coreDetails.AppendLine($"        Name: {cc.Name}");
                        coreDetails.AppendLine($"        Percent Processor Time: {cc.PercentProcessorTime}\n");
                        count++;
                    }
                    cpuCharacteristics.Add($"    CPU Core List:\n{coreDetails.ToString().Trim()}");

                    cpuCharacteristics.Add($"    ProcessorId: {cpu.ProcessorId}");
                    cpuCharacteristics.Add($"    Stock-keeping Unit: {cpu.SocketDesignation}");
                    cpuCharacteristics.Add($"    CurrentClockSpeed: {cpu.CurrentClockSpeed}");
                    cpuCharacteristics.Add($"    MaxClockSpeed: {cpu.MaxClockSpeed}");
                    cpuCharacteristics.Add($"    NumberOfCores: {cpu.NumberOfCores}");
                    cpuCharacteristics.Add($"    NumberOfLogicalProcessors: {cpu.NumberOfLogicalProcessors}");
                    cpuCharacteristics.Add($"    L1DataCacheSize: {cpu.L1DataCacheSize}");
                    cpuCharacteristics.Add($"    L1InstructionCacheSize: {cpu.L1InstructionCacheSize}");
                    cpuCharacteristics.Add($"    L2CacheSize: {cpu.L2CacheSize}");
                    cpuCharacteristics.Add($"    L3CacheSize: {cpu.L3CacheSize}");
                    cpuCharacteristics.Add($"    Percent Processor Time: {cpu.PercentProcessorTime}");
                    cpuCharacteristics.Add($"    Second Level Address Translation Extensions: {cpu.SecondLevelAddressTranslationExtensions}");
                    cpuCharacteristics.Add($"    Virtualization Firmware Enabled: {cpu.VirtualizationFirmwareEnabled}");
                    cpuCharacteristics.Add($"    VM Monitor Mode Extensions: {cpu.VMMonitorModeExtensions}");
                    cpus[$"- CPU #{counter.Count}:"] = cpuCharacteristics;
                    counter.Count++;
                }

                // Print CPU Characteristics
                foreach (var cpu in cpus.Keys)
                {
                    Console.WriteLine(cpu);
                    foreach (string charac in cpus[cpu])
                    {
                        Console.WriteLine(charac);
                    }
                }

                // Add to content dictionary
                content[cpuKey] = cpus;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region Drives
                // Drive List
                string driveKey = "Drive List:";
                Console.WriteLine(driveKey);
                Dictionary<string, List<string>> drives = new Dictionary<string, List<string>>();
                counter.Count = 1;

                foreach (var drive in hardwareInfo.DriveList)
                {
                    List<string> driveCharacteristics = new List<string>();
                    driveCharacteristics.Add($"    Name: {drive.Name}");
                    driveCharacteristics.Add($"    Caption: {drive.Caption}");
                    driveCharacteristics.Add($"    Description: {drive.Description}");
                    driveCharacteristics.Add($"    FirmwareRevision: {drive.FirmwareRevision}");
                    driveCharacteristics.Add($"    Manufacturer: {drive.Manufacturer}");
                    driveCharacteristics.Add($"    Model: {drive.Model}");
                    driveCharacteristics.Add($"    SerialNumber: {drive.SerialNumber}");
                    driveCharacteristics.Add($"    Index: {drive.Index}");
                    driveCharacteristics.Add($"    Partitions Count: {drive.Partitions}");

                    // Generate a string for Partition List details
                    StringBuilder partitionDetails = new StringBuilder();
                    int count = 1;
                    foreach (Partition p in drive.PartitionList)
                    {
                        partitionDetails.AppendLine($"        ---{count}---");
                        partitionDetails.AppendLine($"        Name: {p.Name}");
                        partitionDetails.AppendLine($"        Caption: {p.Caption}");
                        partitionDetails.AppendLine($"        Description: {p.Description}");
                        partitionDetails.AppendLine($"        Index: {p.Index}");
                        partitionDetails.AppendLine($"        Disk Index: {p.DiskIndex}");
                        partitionDetails.AppendLine($"        Bootable: {p.Bootable}");
                        partitionDetails.AppendLine($"        Boot Partition: {p.BootPartition}");
                        partitionDetails.AppendLine($"        Primary Partition: {p.PrimaryPartition}");
                        partitionDetails.AppendLine($"        Size: {p.Size} bytes");
                        partitionDetails.AppendLine($"        Starting Offset: {p.StartingOffset} bytes");
                        partitionDetails.AppendLine($"        Volume List: {string.Join(", ", p.VolumeList)}\n");
                        count++;
                    }
                    driveCharacteristics.Add($"    Partition List:\n{partitionDetails.ToString().Trim()}");

                    driveCharacteristics.Add($"    Size: {drive.Size}");
                    drives[$"- Drive #{counter.Count}:"] = driveCharacteristics;
                    counter.Count++;
                }

                // Print Drive Characteristics
                foreach (var drive in drives.Keys)
                {
                    Console.WriteLine(drive);
                    foreach (string charac in drives[drive])
                    {
                        Console.WriteLine(charac);
                    }
                }

                // Add to content dictionary
                content[driveKey] = drives;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region Memory
                // Memory List
                string memoryKey = "Memory List:";
                Console.WriteLine(memoryKey);
                Dictionary<string, List<string>> memories = new Dictionary<string, List<string>>();
                counter.Count = 1; // Reset counter for Memory list
                foreach (var memory in hardwareInfo.MemoryList)
                {
                    List<string> memoryCharacteristics = new List<string>();
                    memoryCharacteristics.Add($"    BankLabel: {memory.BankLabel}");
                    memoryCharacteristics.Add($"    Capacity: {memory.Capacity}");
                    memoryCharacteristics.Add($"    Form Factor: {memory.FormFactor}");
                    memoryCharacteristics.Add($"    Manufacturer: {memory.Manufacturer}");
                    memoryCharacteristics.Add($"    Min Voltage: {memory.MinVoltage}");
                    memoryCharacteristics.Add($"    Max Voltage: {memory.MaxVoltage}");
                    memoryCharacteristics.Add($"    PartNumber: {memory.PartNumber}");
                    memoryCharacteristics.Add($"    SerialNumber: {memory.SerialNumber}");
                    memoryCharacteristics.Add($"    Speed: {memory.Speed}");
                    memories[$"- Memory #{counter.Count}:"] = memoryCharacteristics;
                    counter.Count++;
                }

                // Print Memory characteristics
                foreach (var memory in memories.Keys)
                {
                    Console.WriteLine(memory);
                    foreach (string charac in memories[memory])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[memoryKey] = memories;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region Monitors
                // Monitor List
                string monitorKey = "Monitor List:";
                Console.WriteLine(monitorKey);
                Dictionary<string, List<string>> monitors = new Dictionary<string, List<string>>();
                counter.Count = 1;
                foreach (var monitor in hardwareInfo.MonitorList)
                {
                    List<string> monitorCharacteristics = new List<string>();
                    monitorCharacteristics.Add($"    Monitor Name: {monitor.Name}");
                    monitorCharacteristics.Add($"    User-Friendly Name: {monitor.UserFriendlyName}");
                    monitorCharacteristics.Add($"    Monitor Caption: {monitor.Caption}");
                    monitorCharacteristics.Add($"    Monitor Description: {monitor.Description}");
                    monitorCharacteristics.Add($"    Monitor Active: {monitor.Active}");
                    monitorCharacteristics.Add($"    Manufacturer Name: {monitor.ManufacturerName}");
                    monitorCharacteristics.Add($"    Monitor Manufacturer: {monitor.MonitorManufacturer}");
                    monitorCharacteristics.Add($"    Monitor Type: {monitor.MonitorType}");
                    monitorCharacteristics.Add($"    Pixels Per X Logical Inch: {monitor.PixelsPerXLogicalInch}");
                    monitorCharacteristics.Add($"    Pixels Per Y Logical Inch: {monitor.PixelsPerYLogicalInch}");
                    monitorCharacteristics.Add($"    Product Code ID: {monitor.ProductCodeID}");
                    monitorCharacteristics.Add($"    Serial Number ID: {monitor.SerialNumberID}");
                    monitorCharacteristics.Add($"    Year of Manufacture: {monitor.YearOfManufacture}");
                    monitorCharacteristics.Add($"    Week of Manufacture: {monitor.WeekOfManufacture}");
                    monitors[$"- Monitor #{counter.Count}:"] = monitorCharacteristics;
                    counter.Count++;
                }

                foreach (var monitor in monitors.Keys)
                {
                    Console.WriteLine(monitor);
                    foreach (string charac in monitors[monitor])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[monitorKey] = monitors;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region Keyboards
                // Keyboard List
                string keyboardKey = "Keyboard List:";
                Console.WriteLine(keyboardKey);
                Dictionary<string, List<string>> keyboards = new Dictionary<string, List<string>>();
                counter.Count = 1;
                foreach (var keyboard in hardwareInfo.KeyboardList)
                {
                    List<string> keyboardCharacteristics = new List<string>();
                    keyboardCharacteristics.Add($"    Keyboard Name: {keyboard.Name}");
                    keyboardCharacteristics.Add($"    Keyboard Caption: {keyboard.Caption}");
                    keyboardCharacteristics.Add($"    Keyboard Description: {keyboard.Description}");
                    keyboardCharacteristics.Add($"    Number of Function Keys: {keyboard.NumberOfFunctionKeys}");
                    keyboards[$"- Keyboard #{counter.Count}:"] = keyboardCharacteristics;
                    counter.Count++;
                }

                foreach (var keyboard in keyboards.Keys)
                {
                    Console.WriteLine(keyboard);
                    foreach (string charac in keyboards[keyboard])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[keyboardKey] = keyboards;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region Printers

                // Printer List
                string printerKey = "Printer List:";
                Console.WriteLine(printerKey);
                Dictionary<string, List<string>> printers = new Dictionary<string, List<string>>();
                counter.Count = 1;
                foreach (var printer in hardwareInfo.PrinterList)
                {
                    List<string> printerCharacteristics = new List<string>();
                    printerCharacteristics.Add($"    Name: {printer.Name}");
                    printerCharacteristics.Add($"    Caption: {printer.Caption}");
                    printerCharacteristics.Add($"    Description: {printer.Description}");
                    printerCharacteristics.Add($"    Default Printer: {printer.Default}");
                    printerCharacteristics.Add($"    Horizontal Resolution: {printer.HorizontalResolution}");
                    printerCharacteristics.Add($"    Vertical Resolution: {printer.VerticalResolution}");
                    printerCharacteristics.Add($"    Local Printer: {printer.Local}");
                    printerCharacteristics.Add($"    Network Printer: {printer.Network}");
                    printerCharacteristics.Add($"    Shared Printer: {printer.Shared}");
                    printers[$"- Printer #{counter.Count}:"] = printerCharacteristics;
                    counter.Count++;
                }

                foreach (var printer in printers.Keys)
                {
                    Console.WriteLine(printer);
                    foreach (string charac in printers[printer])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[printerKey] = printers;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region Mice
                // Mouse List
                string mouseKey = "Mouse List:";
                Console.WriteLine(mouseKey);
                Dictionary<string, List<string>> mice = new Dictionary<string, List<string>>();
                counter.Count = 1;
                foreach (var mouse in hardwareInfo.MouseList)
                {
                    List<string> mouseCharacteristics = new List<string>();
                    mouseCharacteristics.Add($"    Name: {mouse.Name}");
                    mouseCharacteristics.Add($"    Caption: {mouse.Caption}");
                    mouseCharacteristics.Add($"    Description: {mouse.Description}");
                    mouseCharacteristics.Add($"    Number of buttons: {mouse.NumberOfButtons}");
                    mouseCharacteristics.Add($"    Manufacturer: {mouse.Manufacturer}");
                    mice[$"- Mouse #{counter.Count}:"] = mouseCharacteristics;
                    counter.Count++;
                }

                foreach (var mouse in mice.Keys)
                {
                    Console.WriteLine(mouse);
                    foreach (string charac in mice[mouse])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[mouseKey] = mice;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region Motherboards
                // Motherboard List
                string motherboardKey = "Motherboard List:";
                Console.WriteLine(motherboardKey);
                Dictionary<string, List<string>> motherboards = new Dictionary<string, List<string>>();
                counter.Count = 1;
                foreach (var motherboard in hardwareInfo.MotherboardList)
                {
                    List<string> motherboardCharacteristics = new List<string>();
                    motherboardCharacteristics.Add($"    Serial Number: {motherboard.SerialNumber}");
                    motherboardCharacteristics.Add($"    Baseboard part number: {motherboard.Product}");
                    motherboardCharacteristics.Add($"    Manufacturer: {motherboard.Manufacturer}");
                    motherboards[$"- Motherboard #{counter.Count}:"] = motherboardCharacteristics;
                    counter.Count++;
                }

                foreach (var motherboard in motherboards.Keys)
                {
                    Console.WriteLine(motherboard);
                    foreach (string charac in motherboards[motherboard])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[motherboardKey] = motherboards;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region NetworkAdapters
                // Network Adapter List
                string networkAdapterKey = "Network Adapter List:";
                Console.WriteLine(networkAdapterKey);
                Dictionary<string, List<string>> networkAdapters = new Dictionary<string, List<string>>();
                counter.Count = 1;
                foreach (var networkAdapter in hardwareInfo.NetworkAdapterList)
                {
                    List<string> networkAdapterCharacteristics = new List<string>();
                    networkAdapterCharacteristics.Add($"    Name: {networkAdapter.Name}");
                    networkAdapterCharacteristics.Add($"    Caption: {networkAdapter.Caption}");
                    networkAdapterCharacteristics.Add($"    Product Name: {networkAdapter.ProductName}");
                    networkAdapterCharacteristics.Add($"    Manufacturer: {networkAdapter.Manufacturer}");
                    networkAdapterCharacteristics.Add($"    Description: {networkAdapter.Description}");
                    networkAdapterCharacteristics.Add($"    Adapter Type: {networkAdapter.AdapterType}");
                    networkAdapterCharacteristics.Add($"    MAC Address: {networkAdapter.MACAddress}");
                    networkAdapterCharacteristics.Add($"    Net Connection ID: {networkAdapter.NetConnectionID}");
                    networkAdapterCharacteristics.Add($"    Speed: {networkAdapter.Speed} bytes/sec");
                    networkAdapterCharacteristics.Add($"    IP Address List: {string.Join(", ", networkAdapter.IPAddressList)}");
                    networkAdapterCharacteristics.Add($"    IP Subnet List: {string.Join(", ", networkAdapter.IPSubnetList)}");
                    networkAdapterCharacteristics.Add($"    Default IP Gateway List: {string.Join(", ", networkAdapter.DefaultIPGatewayList)}");
                    networkAdapterCharacteristics.Add($"    DNS Server Search Order List: {string.Join(", ", networkAdapter.DNSServerSearchOrderList)}");
                    networkAdapterCharacteristics.Add($"    DHCP Server: {networkAdapter.DHCPServer}");
                    networkAdapterCharacteristics.Add($"    Bytes Received Per Second: {networkAdapter.BytesReceivedPersec}");
                    networkAdapterCharacteristics.Add($"    Bytes Sent Per Second: {networkAdapter.BytesSentPersec}");
                    networkAdapters[$"- Network Adapter #{counter.Count}:"] = networkAdapterCharacteristics;
                    counter.Count++;
                }

                foreach (var networkAdapter in networkAdapters.Keys)
                {
                    Console.WriteLine(networkAdapter);
                    foreach (string charac in networkAdapters[networkAdapter])
                    {
                        Console.WriteLine(charac);
                    }
                }

                content[networkAdapterKey] = networkAdapters;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region OperatingSystem
                // Operating System
                string operatingSystemKey = "Operating System:";
                Console.WriteLine(operatingSystemKey);
                Dictionary<string, List<string>> operatingSystemInfo = new Dictionary<string, List<string>>();

                List<string> osCharacteristics = new List<string>();
                osCharacteristics.Add($"    Name: {hardwareInfo.OperatingSystem.Name}");
                osCharacteristics.Add($"    Version: {hardwareInfo.OperatingSystem.VersionString}");

                operatingSystemInfo["- OS:"] = osCharacteristics;

                // Print Operating System Characteristics
                foreach (var osKey in operatingSystemInfo.Keys)
                {
                    Console.WriteLine(osKey);
                    foreach (string charac in operatingSystemInfo[osKey])
                    {
                        Console.WriteLine(charac);
                    }
                }

                // Add to content dictionary
                content[operatingSystemKey] = operatingSystemInfo;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region SoundDevices
                // Sound Device List
                string soundDeviceKey = "Sound Device List:";
                Console.WriteLine(soundDeviceKey);
                Dictionary<string, List<string>> soundDevices = new Dictionary<string, List<string>>();
                counter.Count = 1;

                foreach (var soundDevice in hardwareInfo.SoundDeviceList)
                {
                    List<string> soundDeviceCharacteristics = new List<string>();
                    soundDeviceCharacteristics.Add($"    Name: {soundDevice.Name}");
                    soundDeviceCharacteristics.Add($"    Caption: {soundDevice.Caption}");
                    soundDeviceCharacteristics.Add($"    Product Name: {soundDevice.ProductName}");
                    soundDeviceCharacteristics.Add($"    Description: {soundDevice.Description}");
                    soundDeviceCharacteristics.Add($"    Manufacturer: {soundDevice.Manufacturer}");
                    soundDevices[$"- Sound Device #{counter.Count}:"] = soundDeviceCharacteristics;
                    counter.Count++;
                }

                // Print Sound Device Characteristics
                foreach (var soundDevice in soundDevices.Keys)
                {
                    Console.WriteLine(soundDevice);
                    foreach (string charac in soundDevices[soundDevice])
                    {
                        Console.WriteLine(charac);
                    }
                }

                // Add to content dictionary
                content[soundDeviceKey] = soundDevices;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region VideoControllers
                // Video Controller List
                string videoControllerKey = "Video Controller List:";
                Console.WriteLine(videoControllerKey);
                Dictionary<string, List<string>> videoControllers = new Dictionary<string, List<string>>();
                counter.Count = 1;

                foreach (var videoController in hardwareInfo.VideoControllerList)
                {
                    List<string> videoControllerCharacteristics = new List<string>();
                    videoControllerCharacteristics.Add($"    Name: {videoController.Name}");
                    videoControllerCharacteristics.Add($"    Caption: {videoController.Caption}");
                    videoControllerCharacteristics.Add($"    Description: {videoController.Description}");
                    videoControllerCharacteristics.Add($"    Manufacturer: {videoController.Manufacturer}");
                    videoControllerCharacteristics.Add($"    Adapter RAM: {videoController.AdapterRAM} bytes");
                    videoControllerCharacteristics.Add($"    Video Processor: {videoController.VideoProcessor}");
                    videoControllerCharacteristics.Add($"    Video Mode Description: {videoController.VideoModeDescription}");
                    videoControllerCharacteristics.Add($"    Current Horizontal Resolution: {videoController.CurrentHorizontalResolution}");
                    videoControllerCharacteristics.Add($"    Current Vertical Resolution: {videoController.CurrentVerticalResolution}");
                    videoControllerCharacteristics.Add($"    Current Bits Per Pixel: {videoController.CurrentBitsPerPixel}");
                    videoControllerCharacteristics.Add($"    Current Number of Colors: {videoController.CurrentNumberOfColors}");
                    videoControllerCharacteristics.Add($"    Current Refresh Rate: {videoController.CurrentRefreshRate} Hz");
                    videoControllerCharacteristics.Add($"    Max Refresh Rate: {videoController.MaxRefreshRate} Hz");
                    videoControllerCharacteristics.Add($"    Min Refresh Rate: {videoController.MinRefreshRate} Hz");
                    videoControllerCharacteristics.Add($"    Driver Version: {videoController.DriverVersion}");
                    videoControllerCharacteristics.Add($"    Driver Date: {videoController.DriverDate}");
                    videoControllers[$"- Video Controller #{counter.Count}:"] = videoControllerCharacteristics;
                    counter.Count++;
                }

                // Print Video Controller Characteristics
                foreach (var videoController in videoControllers.Keys)
                {
                    Console.WriteLine(videoController);
                    foreach (string charac in videoControllers[videoController])
                    {
                        Console.WriteLine(charac);
                    }
                }

                // Add to content dictionary
                content[videoControllerKey] = videoControllers;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                #region ComputerSystems
                // Computer System List
                string computerSystemKey = "Computer System List:";
                Console.WriteLine(computerSystemKey);
                Dictionary<string, List<string>> computerSystems = new Dictionary<string, List<string>>();
                counter.Count = 1;

                foreach (var computerSystem in hardwareInfo.ComputerSystemList)
                {
                    List<string> computerSystemCharacteristics = new List<string>();
                    computerSystemCharacteristics.Add($"    Name: {computerSystem.Name}");
                    computerSystemCharacteristics.Add($"    Caption: {computerSystem.Caption}");
                    computerSystemCharacteristics.Add($"    Description: {computerSystem.Description}");
                    computerSystemCharacteristics.Add($"    Version: {computerSystem.Version}");
                    computerSystemCharacteristics.Add($"    Vendor: {computerSystem.Vendor}");
                    computerSystemCharacteristics.Add($"    Identifying Number: {computerSystem.IdentifyingNumber}");
                    computerSystemCharacteristics.Add($"    SKU Number: {computerSystem.SKUNumber}");
                    computerSystemCharacteristics.Add($"    Universally Unique Identifier (UUID): {computerSystem.UUID}");
                    computerSystems[$"- Computer System #{counter.Count}:"] = computerSystemCharacteristics;
                    counter.Count++;
                }

                // Print Computer System Characteristics
                foreach (var computerSystem in computerSystems.Keys)
                {
                    Console.WriteLine(computerSystem);
                    foreach (string charac in computerSystems[computerSystem])
                    {
                        Console.WriteLine(charac);
                    }
                }

                // Add to content dictionary
                content[computerSystemKey] = computerSystems;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");
                #endregion

                #region MemoryStatus
                // Memory Status
                string memoryStatusKey = "Memory Status:";
                Console.WriteLine(memoryStatusKey);
                Dictionary<string, List<string>> memoryStatus = new Dictionary<string, List<string>>();

                List<string> memStatCharacteristics = new List<string>();
                memStatCharacteristics.Add($"    Available Extended Virtual Memory: {hardwareInfo.MemoryStatus.AvailableExtendedVirtual}");
                memStatCharacteristics.Add($"    Available Page File: {hardwareInfo.MemoryStatus.AvailablePageFile}");
                memStatCharacteristics.Add($"    Available Physical Memory: {hardwareInfo.MemoryStatus.AvailablePhysical}");
                memStatCharacteristics.Add($"    Available Virtual Memory: {hardwareInfo.MemoryStatus.AvailableVirtual}");
                memStatCharacteristics.Add($"    Total Page File: {hardwareInfo.MemoryStatus.TotalPageFile}");
                memStatCharacteristics.Add($"    Total Physical Memory: {hardwareInfo.MemoryStatus.TotalPhysical}");
                memStatCharacteristics.Add($"    Total Virtual Memory: {hardwareInfo.MemoryStatus.TotalVirtual}");

                memoryStatus["- MemStat:"] = memStatCharacteristics;

                // Print Memory Status Characteristics
                foreach (var memStatKey in memoryStatus.Keys)
                {
                    Console.WriteLine(memStatKey);
                    foreach (string charac in memoryStatus[memStatKey])
                    {
                        Console.WriteLine(charac);
                    }
                }

                // Add to content dictionary
                content[memoryStatusKey] = memoryStatus;

                Console.WriteLine("*****************************");
                Console.WriteLine();
                Console.WriteLine("*****************************");

                #endregion

                bool result = true;
                if (writeToFile)
                {
                    if (fileLocation == null || fileLocation == "")
                    {
                        Random random = new Random();
                        result = FileProcessor.FileWriting.WriteToFile(
                            content, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +
                            $"/computer_information_{random.Next(10000)}.txt");
                    }
                    else
                    {
                        Random random = new Random();
                        result = FileProcessor.FileWriting.WriteToFile(
                            content, fileLocation +
                            $"/computer_information_{random.Next(10000)}.txt");
                    }

                }
                return result;
            }
            catch (Exception e)
            {
                ConsoleAlert.ErrorInternal();
                ErrorLog.Write(e.Message, DateTime.Now);
                return false;
            }
            
        }
    }
}
