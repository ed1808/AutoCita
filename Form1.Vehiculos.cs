using AutoCita.Abstractions;
using AutoCita.Models;

namespace AutoCita
{
    internal partial class Form1
    {
        // --- Vehicle Form Controls ---
        private ComboBox cmbVehiculoTipo = null!;
        private ComboBox cmbVehiculoPropietario = null!;
        private TextBox txtVehiculoVin = null!;
        private TextBox txtVehiculoPlaca = null!;
        private TextBox txtVehiculoMarca = null!;
        private TextBox txtVehiculoLinea = null!;
        private NumericUpDown nudVehiculoModelo = null!;
        private NumericUpDown nudVehiculoKilometraje = null!;

        // Type-specific panel
        private Panel pnlVehiculoExtra = null!;

        // Automovil fields
        private NumericUpDown? nudAutoPuertas;
        private TextBox? txtAutoCombustible;
        private TextBox? txtAutoTransmision;

        // Camion fields
        private NumericUpDown? nudCamionEjes;
        private TextBox? txtCamionTipoCarga;
        private NumericUpDown? nudCamionCapacidad;

        // Camioneta fields
        private CheckBox? chkCamionetaDobleTraccion;
        private NumericUpDown? nudCamionetaCapacidad;

        // Motocicleta fields
        private TextBox? txtMotoTipo;

        private Button btnVehiculoAgregar = null!;
        private Button btnVehiculoActualizar = null!;
        private Button btnVehiculoLimpiar = null!;
        private DataGridView dgvVehiculos = null!;

        private Guid? _selectedVehiculoId;
        private Dictionary<Guid, Vehiculo> _vehiculosCache = [];

        private void BuildVehiculosPage()
        {
            var lblTitulo = new Label
            {
                Text = "  Gestión de Vehículos",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 45, 48),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var formPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 260,
                Padding = new Padding(12, 4, 12, 8)
            };

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 7
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 7; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));

            // Row 0 - Tipo y Propietario
            cmbVehiculoTipo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            cmbVehiculoTipo.Items.AddRange(["Automóvil", "Camión", "Camioneta", "Motocicleta"]);
            cmbVehiculoTipo.SelectedIndex = 0;

            cmbVehiculoPropietario = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };

            table.Controls.Add(CreateFieldLabel("Tipo:"), 0, 0);
            table.Controls.Add(cmbVehiculoTipo, 1, 0);
            table.Controls.Add(CreateFieldLabel("Propietario:"), 2, 0);
            table.Controls.Add(cmbVehiculoPropietario, 3, 0);

            // Row 1 - VIN y Placa
            txtVehiculoVin = CreateTextBox();
            txtVehiculoPlaca = CreateTextBox();
            table.Controls.Add(CreateFieldLabel("VIN:"), 0, 1);
            table.Controls.Add(txtVehiculoVin, 1, 1);
            table.Controls.Add(CreateFieldLabel("Placa:"), 2, 1);
            table.Controls.Add(txtVehiculoPlaca, 3, 1);

            // Row 2 - Marca y Línea
            txtVehiculoMarca = CreateTextBox();
            txtVehiculoLinea = CreateTextBox();
            table.Controls.Add(CreateFieldLabel("Marca:"), 0, 2);
            table.Controls.Add(txtVehiculoMarca, 1, 2);
            table.Controls.Add(CreateFieldLabel("Línea:"), 2, 2);
            table.Controls.Add(txtVehiculoLinea, 3, 2);

            // Row 3 - Modelo y Kilometraje
            nudVehiculoModelo = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 1900,
                Maximum = 2030,
                Value = DateTime.Now.Year,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            nudVehiculoKilometraje = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 9999999,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            table.Controls.Add(CreateFieldLabel("Modelo (Año):"), 0, 3);
            table.Controls.Add(nudVehiculoModelo, 1, 3);
            table.Controls.Add(CreateFieldLabel("Kilometraje:"), 2, 3);
            table.Controls.Add(nudVehiculoKilometraje, 3, 3);

            // Rows 4-5 - Dynamic type-specific fields
            pnlVehiculoExtra = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            table.Controls.Add(pnlVehiculoExtra, 0, 4);
            table.SetColumnSpan(pnlVehiculoExtra, 4);
            table.SetRowSpan(pnlVehiculoExtra, 2);

            BuildVehiculoExtraFields();

            // Row 6 - Buttons
            var btnPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0)
            };
            btnVehiculoAgregar = CreateActionButton("Agregar", Color.FromArgb(0, 122, 204));
            btnVehiculoActualizar = CreateActionButton("Actualizar", Color.FromArgb(0, 150, 80));
            btnVehiculoActualizar.Enabled = false;
            btnVehiculoLimpiar = CreateActionButton("Limpiar", Color.FromArgb(108, 117, 125));
            btnPanel.Controls.AddRange([btnVehiculoAgregar, btnVehiculoActualizar, btnVehiculoLimpiar]);
            table.Controls.Add(btnPanel, 0, 6);
            table.SetColumnSpan(btnPanel, 4);

            formPanel.Controls.Add(table);

            // Grid
            dgvVehiculos = CreateDataGridView();
            dgvVehiculos.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Visible = false },
                new DataGridViewTextBoxColumn { Name = "Tipo", HeaderText = "Tipo", FillWeight = 80 },
                new DataGridViewTextBoxColumn { Name = "Placa", HeaderText = "Placa", FillWeight = 70 },
                new DataGridViewTextBoxColumn { Name = "Marca", HeaderText = "Marca", FillWeight = 80 },
                new DataGridViewTextBoxColumn { Name = "Linea", HeaderText = "Línea", FillWeight = 80 },
                new DataGridViewTextBoxColumn { Name = "Modelo", HeaderText = "Modelo", FillWeight = 50 },
                new DataGridViewTextBoxColumn { Name = "Km", HeaderText = "Km", FillWeight = 60 },
                new DataGridViewTextBoxColumn { Name = "Propietario", HeaderText = "Propietario", FillWeight = 120 },
                new DataGridViewTextBoxColumn { Name = "Detalles", HeaderText = "Detalles", FillWeight = 150 }
            );

            pageVehiculos.Controls.Add(dgvVehiculos);
            pageVehiculos.Controls.Add(formPanel);
            pageVehiculos.Controls.Add(lblTitulo);

            // Events
            cmbVehiculoTipo.SelectedIndexChanged += (_, _) => BuildVehiculoExtraFields();
            btnVehiculoAgregar.Click += async (_, _) => await AgregarVehiculo();
            btnVehiculoActualizar.Click += async (_, _) => await ActualizarVehiculoAsync();
            btnVehiculoLimpiar.Click += (_, _) => LimpiarFormVehiculo();
            dgvVehiculos.SelectionChanged += DgvVehiculos_SelectionChanged;
        }

        private void BuildVehiculoExtraFields()
        {
            pnlVehiculoExtra.Controls.Clear();

            var extraTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2
            };
            extraTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            extraTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            extraTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            extraTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            extraTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            extraTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));

            var tipo = cmbVehiculoTipo.SelectedItem?.ToString() ?? "Automóvil";

            switch (tipo)
            {
                case "Automóvil":
                    nudAutoPuertas = new NumericUpDown
                    {
                        Dock = DockStyle.Fill, Minimum = 1, Maximum = 6, Value = 4,
                        Font = new Font("Segoe UI", 9.5f), Margin = new Padding(4)
                    };
                    txtAutoCombustible = CreateTextBox();
                    txtAutoTransmision = CreateTextBox();
                    extraTable.Controls.Add(CreateFieldLabel("N° Puertas:"), 0, 0);
                    extraTable.Controls.Add(nudAutoPuertas, 1, 0);
                    extraTable.Controls.Add(CreateFieldLabel("Combustible:"), 2, 0);
                    extraTable.Controls.Add(txtAutoCombustible, 3, 0);
                    extraTable.Controls.Add(CreateFieldLabel("Transmisión:"), 0, 1);
                    extraTable.Controls.Add(txtAutoTransmision, 1, 1);
                    break;

                case "Camión":
                    nudCamionEjes = new NumericUpDown
                    {
                        Dock = DockStyle.Fill, Minimum = 2, Maximum = 12, Value = 2,
                        Font = new Font("Segoe UI", 9.5f), Margin = new Padding(4)
                    };
                    txtCamionTipoCarga = CreateTextBox();
                    nudCamionCapacidad = new NumericUpDown
                    {
                        Dock = DockStyle.Fill, Minimum = 0, Maximum = 100000, DecimalPlaces = 2,
                        Font = new Font("Segoe UI", 9.5f), Margin = new Padding(4)
                    };
                    extraTable.Controls.Add(CreateFieldLabel("N° Ejes:"), 0, 0);
                    extraTable.Controls.Add(nudCamionEjes, 1, 0);
                    extraTable.Controls.Add(CreateFieldLabel("Tipo Carga:"), 2, 0);
                    extraTable.Controls.Add(txtCamionTipoCarga, 3, 0);
                    extraTable.Controls.Add(CreateFieldLabel("Cap. Carga (ton):"), 0, 1);
                    extraTable.Controls.Add(nudCamionCapacidad, 1, 1);
                    break;

                case "Camioneta":
                    chkCamionetaDobleTraccion = new CheckBox
                    {
                        Text = "Doble Tracción",
                        Dock = DockStyle.Fill,
                        Font = new Font("Segoe UI", 9.5f),
                        Margin = new Padding(4)
                    };
                    nudCamionetaCapacidad = new NumericUpDown
                    {
                        Dock = DockStyle.Fill, Minimum = 0, Maximum = 100000, DecimalPlaces = 2,
                        Font = new Font("Segoe UI", 9.5f), Margin = new Padding(4)
                    };
                    extraTable.Controls.Add(chkCamionetaDobleTraccion, 0, 0);
                    extraTable.SetColumnSpan(chkCamionetaDobleTraccion, 2);
                    extraTable.Controls.Add(CreateFieldLabel("Cap. Carga (kg):"), 2, 0);
                    extraTable.Controls.Add(nudCamionetaCapacidad, 3, 0);
                    break;

                case "Motocicleta":
                    txtMotoTipo = CreateTextBox();
                    extraTable.Controls.Add(CreateFieldLabel("Tipo Moto:"), 0, 0);
                    extraTable.Controls.Add(txtMotoTipo, 1, 0);
                    break;
            }

            pnlVehiculoExtra.Controls.Add(extraTable);
        }

        private async Task CargarPropietariosEnCombo()
        {
            try
            {
                var clientes = await _customerService.ObtenerClientes();
                var selectedId = (cmbVehiculoPropietario.SelectedItem as ComboItem)?.Id;
                cmbVehiculoPropietario.Items.Clear();
                foreach (var c in clientes)
                {
                    cmbVehiculoPropietario.Items.Add(new ComboItem(c.Id, $"{c.Nombre} {c.Apellido} ({c.NumeroDocumento})"));
                }
                // Restore selection if possible
                if (selectedId.HasValue)
                {
                    for (int i = 0; i < cmbVehiculoPropietario.Items.Count; i++)
                    {
                        if (cmbVehiculoPropietario.Items[i] is ComboItem item && item.Id == selectedId.Value)
                        {
                            cmbVehiculoPropietario.SelectedIndex = i;
                            break;
                        }
                    }
                }
                if (cmbVehiculoPropietario.Items.Count > 0 && cmbVehiculoPropietario.SelectedIndex < 0)
                    cmbVehiculoPropietario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar propietarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarVehiculos()
        {
            try
            {
                var vehiculos = await _vehicleService.ObtenerVehiculos();
                var clientes = await _customerService.ObtenerClientes();
                var clienteNombres = clientes.ToDictionary(c => c.Id, c => $"{c.Nombre} {c.Apellido}");

                _vehiculosCache.Clear();
                dgvVehiculos.Rows.Clear();

                foreach (var v in vehiculos)
                {
                    _vehiculosCache[v.Id] = v;
                    var tipoStr = v switch
                    {
                        Automovil => "Automóvil",
                        Camion => "Camión",
                        Camioneta => "Camioneta",
                        Motocicleta => "Motocicleta",
                        _ => "Desconocido"
                    };

                    var detalles = v switch
                    {
                        Automovil a => $"Puertas: {a.NumeroPuertas}, Comb: {a.TipoCombustible}, Trans: {a.Transmision}",
                        Camion c => $"Ejes: {c.NumeroEjes}, Carga: {c.TipoCarga}, Cap: {c.CapacidadCarga} ton",
                        Camioneta ca => $"4x4: {(ca.EsDobleTraccion ? "Sí" : "No")}, Cap: {ca.CapacidadCarga} kg",
                        Motocicleta m => $"Tipo: {m.Tipo}",
                        _ => ""
                    };

                    clienteNombres.TryGetValue(v.PropietarioId, out var propietarioNombre);

                    dgvVehiculos.Rows.Add(
                        v.Id, tipoStr, v.Placa, v.Marca, v.Linea,
                        v.Modelo, v.Kilometraje, propietarioNombre ?? "N/A", detalles
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AgregarVehiculo()
        {
            try
            {
                if (cmbVehiculoPropietario.SelectedItem is not ComboItem propietario)
                {
                    MessageBox.Show("Seleccione un propietario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var vehiculo = CrearVehiculoDesdeForm(Guid.NewGuid(), propietario.Id);
                if (vehiculo is null) return;

                if (await _vehicleService.RegistrarVehiculo(vehiculo))
                {
                    MessageBox.Show("Vehículo registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormVehiculo();
                    await CargarVehiculos();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el vehículo. Verifique los datos o que la placa no esté duplicada.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ActualizarVehiculoAsync()
        {
            if (_selectedVehiculoId is null) return;

            try
            {
                if (cmbVehiculoPropietario.SelectedItem is not ComboItem propietario)
                {
                    MessageBox.Show("Seleccione un propietario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var vehiculo = CrearVehiculoDesdeForm(_selectedVehiculoId.Value, propietario.Id);
                if (vehiculo is null) return;

                if (await _vehicleService.ActualizarVehiculo(vehiculo))
                {
                    MessageBox.Show("Vehículo actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormVehiculo();
                    await CargarVehiculos();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el vehículo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Vehiculo? CrearVehiculoDesdeForm(Guid id, Guid propietarioId)
        {
            var tipo = cmbVehiculoTipo.SelectedItem?.ToString() ?? "Automóvil";
            var vin = txtVehiculoVin.Text.Trim();
            var placa = txtVehiculoPlaca.Text.Trim();
            var marca = txtVehiculoMarca.Text.Trim();
            var linea = txtVehiculoLinea.Text.Trim();
            var modelo = (int)nudVehiculoModelo.Value;
            var km = (int)nudVehiculoKilometraje.Value;

            return tipo switch
            {
                "Automóvil" => new Automovil(id, vin, placa, marca, linea, modelo, km, propietarioId,
                    (int)(nudAutoPuertas?.Value ?? 4),
                    txtAutoCombustible?.Text.Trim() ?? "",
                    txtAutoTransmision?.Text.Trim() ?? ""),

                "Camión" => new Camion(id, vin, placa, marca, linea, modelo, km, propietarioId,
                    (int)(nudCamionEjes?.Value ?? 2),
                    txtCamionTipoCarga?.Text.Trim() ?? "",
                    (double)(nudCamionCapacidad?.Value ?? 0)),

                "Camioneta" => new Camioneta(id, vin, placa, marca, linea, modelo, km, propietarioId,
                    chkCamionetaDobleTraccion?.Checked ?? false,
                    (double)(nudCamionetaCapacidad?.Value ?? 0)),

                "Motocicleta" => new Motocicleta(id, vin, placa, marca, linea, modelo, km, propietarioId,
                    txtMotoTipo?.Text.Trim() ?? ""),

                _ => null
            };
        }

        private void LimpiarFormVehiculo()
        {
            txtVehiculoVin.Clear();
            txtVehiculoPlaca.Clear();
            txtVehiculoMarca.Clear();
            txtVehiculoLinea.Clear();
            nudVehiculoModelo.Value = DateTime.Now.Year;
            nudVehiculoKilometraje.Value = 0;
            cmbVehiculoTipo.SelectedIndex = 0;
            if (cmbVehiculoPropietario.Items.Count > 0)
                cmbVehiculoPropietario.SelectedIndex = 0;
            _selectedVehiculoId = null;
            btnVehiculoAgregar.Enabled = true;
            btnVehiculoActualizar.Enabled = false;
            dgvVehiculos.ClearSelection();
            BuildVehiculoExtraFields();
        }

        private void DgvVehiculos_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvVehiculos.SelectedRows.Count == 0)
                return;

            var row = dgvVehiculos.SelectedRows[0];
            if (row.Cells["Id"].Value is not Guid id)
                return;

            _selectedVehiculoId = id;

            if (!_vehiculosCache.TryGetValue(id, out var vehiculo))
                return;

            // Set common fields
            txtVehiculoVin.Text = vehiculo.Vin;
            txtVehiculoPlaca.Text = vehiculo.Placa;
            txtVehiculoMarca.Text = vehiculo.Marca;
            txtVehiculoLinea.Text = vehiculo.Linea;
            nudVehiculoModelo.Value = vehiculo.Modelo;
            nudVehiculoKilometraje.Value = vehiculo.Kilometraje;

            // Set type and rebuild extra fields
            var tipoIndex = vehiculo switch
            {
                Automovil => 0,
                Camion => 1,
                Camioneta => 2,
                Motocicleta => 3,
                _ => 0
            };
            cmbVehiculoTipo.SelectedIndex = tipoIndex;

            // Set propietario
            for (int i = 0; i < cmbVehiculoPropietario.Items.Count; i++)
            {
                if (cmbVehiculoPropietario.Items[i] is ComboItem item && item.Id == vehiculo.PropietarioId)
                {
                    cmbVehiculoPropietario.SelectedIndex = i;
                    break;
                }
            }

            // Set type-specific fields
            switch (vehiculo)
            {
                case Automovil a:
                    if (nudAutoPuertas is not null) nudAutoPuertas.Value = a.NumeroPuertas;
                    if (txtAutoCombustible is not null) txtAutoCombustible.Text = a.TipoCombustible;
                    if (txtAutoTransmision is not null) txtAutoTransmision.Text = a.Transmision;
                    break;
                case Camion c:
                    if (nudCamionEjes is not null) nudCamionEjes.Value = c.NumeroEjes;
                    if (txtCamionTipoCarga is not null) txtCamionTipoCarga.Text = c.TipoCarga;
                    if (nudCamionCapacidad is not null) nudCamionCapacidad.Value = (decimal)c.CapacidadCarga;
                    break;
                case Camioneta ca:
                    if (chkCamionetaDobleTraccion is not null) chkCamionetaDobleTraccion.Checked = ca.EsDobleTraccion;
                    if (nudCamionetaCapacidad is not null) nudCamionetaCapacidad.Value = (decimal)ca.CapacidadCarga;
                    break;
                case Motocicleta m:
                    if (txtMotoTipo is not null) txtMotoTipo.Text = m.Tipo;
                    break;
            }

            btnVehiculoAgregar.Enabled = false;
            btnVehiculoActualizar.Enabled = true;
        }
    }
}
