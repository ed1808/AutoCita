using AutoCita.Enums;
using AutoCita.Models;

namespace AutoCita
{
    internal partial class Form1
    {
        // --- Appointment Form Controls ---
        private ComboBox cmbCitaCliente = null!;
        private ComboBox cmbCitaVehiculo = null!;
        private TextBox txtCitaMotivo = null!;
        private DateTimePicker dtpCitaFechaHora = null!;
        private NumericUpDown nudCitaDuracion = null!;
        private TextBox txtCitaObservaciones = null!;
        private ComboBox cmbCitaEstado = null!;
        private Button btnCitaCambiarEstado = null!;
        private Button btnCitaAgendar = null!;
        private Button btnCitaCancelar = null!;
        private Button btnCitaReprogramar = null!;
        private Button btnCitaLimpiar = null!;
        private DataGridView dgvCitas = null!;

        private Guid? _selectedCitaId;
        private Dictionary<Guid, string> _clienteNombresCache = [];
        private Dictionary<Guid, string> _vehiculoInfoCache = [];

        private void BuildCitasPage()
        {
            var lblTitulo = new Label
            {
                Text = "  Gestión de Citas",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 45, 48),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var formPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 220,
                Padding = new Padding(12, 4, 12, 8)
            };

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 5
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 5; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));

            // Row 0 - Cliente y Vehículo
            cmbCitaCliente = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            cmbCitaVehiculo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            table.Controls.Add(CreateFieldLabel("Cliente:"), 0, 0);
            table.Controls.Add(cmbCitaCliente, 1, 0);
            table.Controls.Add(CreateFieldLabel("Vehículo:"), 2, 0);
            table.Controls.Add(cmbCitaVehiculo, 3, 0);

            // Row 1 - Motivo y Fecha/Hora
            txtCitaMotivo = CreateTextBox();
            dtpCitaFechaHora = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            table.Controls.Add(CreateFieldLabel("Motivo:"), 0, 1);
            table.Controls.Add(txtCitaMotivo, 1, 1);
            table.Controls.Add(CreateFieldLabel("Fecha/Hora:"), 2, 1);
            table.Controls.Add(dtpCitaFechaHora, 3, 1);

            // Row 2 - Duración y Observaciones
            nudCitaDuracion = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 15,
                Maximum = 480,
                Value = 60,
                Increment = 15,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            txtCitaObservaciones = CreateTextBox();
            table.Controls.Add(CreateFieldLabel("Duración (min):"), 0, 2);
            table.Controls.Add(nudCitaDuracion, 1, 2);
            table.Controls.Add(CreateFieldLabel("Observaciones:"), 2, 2);
            table.Controls.Add(txtCitaObservaciones, 3, 2);

            // Row 3 - Cambiar Estado
            cmbCitaEstado = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4),
                Enabled = false
            };
            cmbCitaEstado.Items.AddRange(["EN_PROCESO", "FINALIZADA", "NO_ASISTIO"]);
            cmbCitaEstado.SelectedIndex = 0;

            btnCitaCambiarEstado = CreateActionButton("Cambiar Estado", Color.FromArgb(100, 60, 150));
            btnCitaCambiarEstado.Enabled = false;

            table.Controls.Add(CreateFieldLabel("Nuevo Estado:"), 0, 3);
            table.Controls.Add(cmbCitaEstado, 1, 3);
            table.Controls.Add(btnCitaCambiarEstado, 2, 3);
            table.SetColumnSpan(btnCitaCambiarEstado, 2);

            // Row 4 - Buttons
            var btnPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0)
            };
            btnCitaAgendar = CreateActionButton("Agendar", Color.FromArgb(0, 122, 204));
            btnCitaCancelar = CreateActionButton("Cancelar Cita", Color.FromArgb(200, 50, 50));
            btnCitaCancelar.Enabled = false;
            btnCitaReprogramar = CreateActionButton("Reprogramar", Color.FromArgb(180, 130, 0));
            btnCitaReprogramar.Enabled = false;
            btnCitaLimpiar = CreateActionButton("Limpiar", Color.FromArgb(108, 117, 125));
            btnPanel.Controls.AddRange([btnCitaAgendar, btnCitaCancelar, btnCitaReprogramar, btnCitaLimpiar]);
            table.Controls.Add(btnPanel, 0, 4);
            table.SetColumnSpan(btnPanel, 4);

            formPanel.Controls.Add(table);

            // Grid
            dgvCitas = CreateDataGridView();
            dgvCitas.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Visible = false },
                new DataGridViewTextBoxColumn { Name = "Cliente", HeaderText = "Cliente", FillWeight = 100 },
                new DataGridViewTextBoxColumn { Name = "Vehiculo", HeaderText = "Vehículo", FillWeight = 100 },
                new DataGridViewTextBoxColumn { Name = "Motivo", HeaderText = "Motivo", FillWeight = 100 },
                new DataGridViewTextBoxColumn { Name = "FechaInicio", HeaderText = "Fecha/Hora Inicio", FillWeight = 90 },
                new DataGridViewTextBoxColumn { Name = "Duracion", HeaderText = "Duración", FillWeight = 50 },
                new DataGridViewTextBoxColumn { Name = "FechaFin", HeaderText = "Fecha/Hora Fin", FillWeight = 90 },
                new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", FillWeight = 70 },
                new DataGridViewTextBoxColumn { Name = "Observaciones", HeaderText = "Observaciones", FillWeight = 120 }
            );

            pageCitas.Controls.Add(dgvCitas);
            pageCitas.Controls.Add(formPanel);
            pageCitas.Controls.Add(lblTitulo);

            // Events
            cmbCitaCliente.SelectedIndexChanged += async (_, _) => await CargarVehiculosDeCliente();
            btnCitaAgendar.Click += async (_, _) => await AgendarCita();
            btnCitaCancelar.Click += async (_, _) => await CancelarCitaAsync();
            btnCitaReprogramar.Click += async (_, _) => await ReprogramarCitaAsync();
            btnCitaCambiarEstado.Click += async (_, _) => await CambiarEstadoCitaAsync();
            btnCitaLimpiar.Click += (_, _) => LimpiarFormCita();
            dgvCitas.SelectionChanged += DgvCitas_SelectionChanged;
        }

        private async Task CargarDatosCitas()
        {
            try
            {
                // Load clients for combo and cache
                var clientes = await _customerService.ObtenerClientes();
                _clienteNombresCache.Clear();
                cmbCitaCliente.Items.Clear();
                foreach (var c in clientes)
                {
                    var nombre = $"{c.Nombre} {c.Apellido}";
                    _clienteNombresCache[c.Id] = nombre;
                    cmbCitaCliente.Items.Add(new ComboItem(c.Id, $"{nombre} ({c.NumeroDocumento})"));
                }
                if (cmbCitaCliente.Items.Count > 0)
                    cmbCitaCliente.SelectedIndex = 0;

                // Load vehicles for cache
                var vehiculos = await _vehicleService.ObtenerVehiculos();
                _vehiculoInfoCache.Clear();
                foreach (var v in vehiculos)
                {
                    _vehiculoInfoCache[v.Id] = $"{v.Marca} {v.Linea} - {v.Placa}";
                }

                // Load appointments
                await CargarCitas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de citas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarCitas()
        {
            try
            {
                var citas = await _appointmentService.ObtenerCitas();
                dgvCitas.Rows.Clear();

                foreach (var cita in citas)
                {
                    _clienteNombresCache.TryGetValue(cita.ClienteId, out var clienteNombre);
                    _vehiculoInfoCache.TryGetValue(cita.VehiculoId, out var vehiculoInfo);

                    dgvCitas.Rows.Add(
                        cita.Id,
                        clienteNombre ?? "N/A",
                        vehiculoInfo ?? "N/A",
                        cita.MotivoSolicitud,
                        cita.FechaHoraInicio.ToString("dd/MM/yyyy HH:mm"),
                        $"{cita.DuracionMinutos} min",
                        cita.FechaHoraFin.ToString("dd/MM/yyyy HH:mm"),
                        cita.Estado.ToString(),
                        cita.Observaciones
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar citas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CargarVehiculosDeCliente()
        {
            cmbCitaVehiculo.Items.Clear();

            if (cmbCitaCliente.SelectedItem is not ComboItem clienteItem)
                return;

            try
            {
                var vehiculos = await _vehicleService.ObtenerVehiculosPorCliente(clienteItem.Id);
                foreach (var v in vehiculos)
                {
                    cmbCitaVehiculo.Items.Add(new ComboItem(v.Id, $"{v.Marca} {v.Linea} - {v.Placa}"));
                }
                if (cmbCitaVehiculo.Items.Count > 0)
                    cmbCitaVehiculo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar vehículos del cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AgendarCita()
        {
            try
            {
                if (cmbCitaCliente.SelectedItem is not ComboItem cliente)
                {
                    MessageBox.Show("Seleccione un cliente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbCitaVehiculo.SelectedItem is not ComboItem vehiculo)
                {
                    MessageBox.Show("Seleccione un vehículo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCitaMotivo.Text))
                {
                    MessageBox.Show("Ingrese el motivo de la cita.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cita = new Cita(
                    Guid.NewGuid(),
                    cliente.Id,
                    vehiculo.Id,
                    txtCitaMotivo.Text.Trim(),
                    dtpCitaFechaHora.Value,
                    (int)nudCitaDuracion.Value,
                    EstadoCita.PROGRAMADA,
                    txtCitaObservaciones.Text.Trim()
                );

                if (await _appointmentService.AgendarCita(cita))
                {
                    MessageBox.Show("Cita agendada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormCita();
                    await CargarDatosCitas();
                }
                else
                {
                    MessageBox.Show("No se pudo agendar la cita. Verifique que no haya conflicto de horarios y que el vehículo pertenezca al cliente.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CancelarCitaAsync()
        {
            if (_selectedCitaId is null) return;

            var motivo = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el motivo de cancelación:",
                "Cancelar Cita",
                "");

            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Debe ingresar un motivo de cancelación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (await _appointmentService.CancelarCita(_selectedCitaId.Value, motivo))
                {
                    MessageBox.Show("Cita cancelada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormCita();
                    await CargarCitas();
                }
                else
                {
                    MessageBox.Show("No se pudo cancelar la cita. Solo se pueden cancelar citas que estén programadas o en proceso.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ReprogramarCitaAsync()
        {
            if (_selectedCitaId is null) return;

            var nuevaFecha = dtpCitaFechaHora.Value;
            var confirmacion = MessageBox.Show(
                $"¿Desea reprogramar la cita para {nuevaFecha:dd/MM/yyyy HH:mm}?",
                "Confirmar Reprogramación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                if (await _appointmentService.ReprogramarCita(_selectedCitaId.Value, nuevaFecha))
                {
                    MessageBox.Show("Cita reprogramada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormCita();
                    await CargarCitas();
                }
                else
                {
                    MessageBox.Show("No se pudo reprogramar la cita. Solo se pueden reprogramar citas con estado PROGRAMADA y sin conflictos de horario.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CambiarEstadoCitaAsync()
        {
            if (_selectedCitaId is null || cmbCitaEstado.SelectedItem is null) return;

            var estadoSeleccionado = cmbCitaEstado.SelectedItem.ToString()!;
            if (!Enum.TryParse<EstadoCita>(estadoSeleccionado, out var nuevoEstado))
                return;

            var confirmacion = MessageBox.Show(
                $"¿Desea cambiar el estado de la cita a {estadoSeleccionado.Replace('_', ' ')}?",
                "Confirmar Cambio de Estado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            try
            {
                if (await _appointmentService.ActualizarEstadoCita(_selectedCitaId.Value, nuevoEstado))
                {
                    MessageBox.Show("Estado de la cita actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormCita();
                    await CargarCitas();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el estado. La transición de estado no es válida.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormCita()
        {
            txtCitaMotivo.Clear();
            txtCitaObservaciones.Clear();
            dtpCitaFechaHora.Value = DateTime.Now;
            nudCitaDuracion.Value = 60;
            if (cmbCitaCliente.Items.Count > 0)
                cmbCitaCliente.SelectedIndex = 0;
            _selectedCitaId = null;
            btnCitaAgendar.Enabled = true;
            btnCitaCancelar.Enabled = false;
            btnCitaReprogramar.Enabled = false;
            cmbCitaEstado.Enabled = false;
            btnCitaCambiarEstado.Enabled = false;
            cmbCitaEstado.Items.Clear();
            cmbCitaEstado.Items.AddRange(["EN_PROCESO", "FINALIZADA", "NO_ASISTIO"]);
            cmbCitaEstado.SelectedIndex = 0;
            dgvCitas.ClearSelection();
        }

        private void DgvCitas_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCitas.SelectedRows.Count == 0)
                return;

            var row = dgvCitas.SelectedRows[0];
            if (row.Cells["Id"].Value is not Guid id)
                return;

            _selectedCitaId = id;

            var estadoStr = row.Cells["Estado"].Value?.ToString() ?? "";
            var esProgramada = estadoStr == EstadoCita.PROGRAMADA.ToString();
            var esEnProceso = estadoStr == EstadoCita.EN_PROCESO.ToString();
            var esCancelable = esProgramada || esEnProceso;
            var puedeActualizarEstado = esProgramada || esEnProceso;

            btnCitaAgendar.Enabled = false;
            btnCitaCancelar.Enabled = esCancelable;
            btnCitaReprogramar.Enabled = esProgramada;
            cmbCitaEstado.Enabled = puedeActualizarEstado;
            btnCitaCambiarEstado.Enabled = puedeActualizarEstado;

            // Ajustar opciones del combo según estado actual
            cmbCitaEstado.Items.Clear();
            if (esProgramada)
                cmbCitaEstado.Items.AddRange(["EN_PROCESO", "NO_ASISTIO"]);
            else if (esEnProceso)
                cmbCitaEstado.Items.AddRange(["FINALIZADA"]);
            if (cmbCitaEstado.Items.Count > 0)
                cmbCitaEstado.SelectedIndex = 0;

            // Populate form fields from grid
            txtCitaMotivo.Text = row.Cells["Motivo"].Value?.ToString() ?? "";
            txtCitaObservaciones.Text = row.Cells["Observaciones"].Value?.ToString() ?? "";

            var fechaStr = row.Cells["FechaInicio"].Value?.ToString() ?? "";
            if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out var fecha))
                dtpCitaFechaHora.Value = fecha;

            var duracionStr = row.Cells["Duracion"].Value?.ToString()?.Replace(" min", "") ?? "60";
            if (int.TryParse(duracionStr, out var duracion))
                nudCitaDuracion.Value = duracion;
        }
    }
}
