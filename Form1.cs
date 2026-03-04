using AutoCita.Interfaces;

namespace AutoCita
{
    internal partial class Form1 : Form
    {
        private readonly ICustomerService _customerService;
        private readonly IVehicleService _vehicleService;
        private readonly IAppointmentService _appointmentService;

        private Panel sideNav = null!;
        private Button btnCitas = null!;
        private Button btnClientes = null!;
        private Button btnVehiculos = null!;

        private Panel contentContainer = null!;
        private Panel pageCitas = null!;
        private Panel pageClientes = null!;
        private Panel pageVehiculos = null!;

        public Form1(ICustomerService customerService, IVehicleService vehicleService, IAppointmentService appointmentService)
        {
            _customerService = customerService;
            _vehicleService = vehicleService;
            _appointmentService = appointmentService;

            InicializarUI();
            WireEvents();
            UpdateUIState();
        }

        private void InicializarUI()
        {
            Text = "AutoCita - Sistema de Gestión de Citas Automotrices";
            ClientSize = new Size(1200, 700);
            MinimumSize = new Size(1000, 600);
            StartPosition = FormStartPosition.CenterScreen;

            // --- Side Navigation ---
            sideNav = new Panel
            {
                Dock = DockStyle.Left,
                Width = 180,
                BackColor = Color.FromArgb(45, 45, 48),
                Padding = new Padding(0, 10, 0, 0)
            };

            pageCitas = new Panel { Dock = DockStyle.Fill, Visible = false };
            pageClientes = new Panel { Dock = DockStyle.Fill, Visible = false };
            pageVehiculos = new Panel { Dock = DockStyle.Fill, Visible = false };

            btnCitas = CrearBotonNav("Citas");
            btnClientes = CrearBotonNav("Clientes");
            btnVehiculos = CrearBotonNav("Vehículos");

            sideNav.Controls.Add(btnVehiculos);
            sideNav.Controls.Add(btnClientes);
            sideNav.Controls.Add(btnCitas);

            // --- Content Container ---
            contentContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 245, 245),
                Padding = new Padding(0)
            };

            BuildClientesPage();
            BuildVehiculosPage();
            BuildCitasPage();

            contentContainer.Controls.Add(pageCitas);
            contentContainer.Controls.Add(pageClientes);
            contentContainer.Controls.Add(pageVehiculos);

            Controls.Add(contentContainer);
            Controls.Add(sideNav);
        }

        private void WireEvents()
        {
            btnCitas.Click += async (_, _) => { MostrarPagina(pageCitas); await CargarDatosCitas(); };
            btnClientes.Click += async (_, _) => { MostrarPagina(pageClientes); await CargarClientes(); };
            btnVehiculos.Click += async (_, _) => { MostrarPagina(pageVehiculos); await CargarVehiculos(); await CargarPropietariosEnCombo(); };
        }

        private async void UpdateUIState()
        {
            MostrarPagina(pageCitas);
            await CargarDatosCitas();
        }

        private void MostrarPagina(Panel pagina)
        {
            pageCitas.Visible = pageCitas == pagina;
            pageClientes.Visible = pageClientes == pagina;
            pageVehiculos.Visible = pageVehiculos == pagina;

            foreach (Control ctrl in sideNav.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = btn.Tag as Panel == pagina
                        ? Color.FromArgb(0, 122, 204)
                        : Color.FromArgb(45, 45, 48);
                }
            }
        }

        private Button CrearBotonNav(string texto)
        {
            Panel pagina = texto switch
            {
                "Citas" => pageCitas,
                "Clientes" => pageClientes,
                "Vehículos" => pageVehiculos,
                _ => pageCitas
            };

            return new Button
            {
                Text = texto,
                Dock = DockStyle.Top,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 48),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Cursor = Cursors.Hand,
                Tag = pagina,
                FlatAppearance =
                {
                    BorderSize = 0,
                    MouseOverBackColor = Color.FromArgb(62, 62, 66)
                }
            };
        }

        // --- UI Factory Methods ---

        private static Label CreateFieldLabel(string text) => new()
        {
            Text = text,
            Font = new Font("Segoe UI", 9.5f),
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(4, 8, 4, 4)
        };

        private static TextBox CreateTextBox() => new()
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 9.5f),
            Margin = new Padding(4)
        };

        private static Button CreateActionButton(string text, Color backColor) => new()
        {
            Text = text,
            Width = 120,
            Height = 34,
            FlatStyle = FlatStyle.Flat,
            BackColor = backColor,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            Cursor = Cursors.Hand,
            Margin = new Padding(4),
            FlatAppearance = { BorderSize = 0 }
        };

        private static DataGridView CreateDataGridView() => new()
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None,
            RowHeadersVisible = false,
            Font = new Font("Segoe UI", 9),
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(45, 45, 48),
                Padding = new Padding(4)
            },
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Padding = new Padding(4),
                SelectionBackColor = Color.FromArgb(0, 122, 204),
                SelectionForeColor = Color.White
            },
            EnableHeadersVisualStyles = false
        };

        /// <summary>
        /// Helper class for ComboBox items that store a Guid ID with display text.
        /// </summary>
        private class ComboItem
        {
            public Guid Id { get; }
            public string DisplayText { get; }

            public ComboItem(Guid id, string displayText)
            {
                Id = id;
                DisplayText = displayText;
            }

            public override string ToString() => DisplayText;
        }
    }
}
