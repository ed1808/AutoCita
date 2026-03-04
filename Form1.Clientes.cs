using AutoCita.Enums;
using AutoCita.Models;

namespace AutoCita
{
    internal partial class Form1
    {
        // --- Client Form Controls ---
        private TextBox txtClienteNombre = null!;
        private TextBox txtClienteApellido = null!;
        private TextBox txtClienteTelefono = null!;
        private TextBox txtClienteEmail = null!;
        private TextBox txtClienteDocumento = null!;
        private ComboBox cmbClienteTipoDoc = null!;
        private DateTimePicker dtpClienteFechaNac = null!;
        private TextBox txtClienteDireccion = null!;
        private Button btnClienteAgregar = null!;
        private Button btnClienteActualizar = null!;
        private Button btnClienteLimpiar = null!;
        private DataGridView dgvClientes = null!;
        private Guid? _selectedClienteId;

        private void BuildClientesPage()
        {
            var lblTitulo = new Label
            {
                Text = "  Gestión de Clientes",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 45, 48),
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var formPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200,
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

            // Row 0
            txtClienteNombre = CreateTextBox();
            txtClienteApellido = CreateTextBox();
            table.Controls.Add(CreateFieldLabel("Nombre:"), 0, 0);
            table.Controls.Add(txtClienteNombre, 1, 0);
            table.Controls.Add(CreateFieldLabel("Apellido:"), 2, 0);
            table.Controls.Add(txtClienteApellido, 3, 0);

            // Row 1
            txtClienteTelefono = CreateTextBox();
            txtClienteEmail = CreateTextBox();
            table.Controls.Add(CreateFieldLabel("Teléfono:"), 0, 1);
            table.Controls.Add(txtClienteTelefono, 1, 1);
            table.Controls.Add(CreateFieldLabel("Email:"), 2, 1);
            table.Controls.Add(txtClienteEmail, 3, 1);

            // Row 2
            txtClienteDocumento = CreateTextBox();
            cmbClienteTipoDoc = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            cmbClienteTipoDoc.DataSource = Enum.GetValues<TipoDocumento>();
            table.Controls.Add(CreateFieldLabel("N° Documento:"), 0, 2);
            table.Controls.Add(txtClienteDocumento, 1, 2);
            table.Controls.Add(CreateFieldLabel("Tipo Doc:"), 2, 2);
            table.Controls.Add(cmbClienteTipoDoc, 3, 2);

            // Row 3
            dtpClienteFechaNac = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(4)
            };
            txtClienteDireccion = CreateTextBox();
            table.Controls.Add(CreateFieldLabel("Fecha Nac:"), 0, 3);
            table.Controls.Add(dtpClienteFechaNac, 1, 3);
            table.Controls.Add(CreateFieldLabel("Dirección:"), 2, 3);
            table.Controls.Add(txtClienteDireccion, 3, 3);

            // Row 4 - Buttons
            var btnPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0)
            };
            btnClienteAgregar = CreateActionButton("Agregar", Color.FromArgb(0, 122, 204));
            btnClienteActualizar = CreateActionButton("Actualizar", Color.FromArgb(0, 150, 80));
            btnClienteActualizar.Enabled = false;
            btnClienteLimpiar = CreateActionButton("Limpiar", Color.FromArgb(108, 117, 125));
            btnPanel.Controls.AddRange([btnClienteAgregar, btnClienteActualizar, btnClienteLimpiar]);
            table.Controls.Add(btnPanel, 0, 4);
            table.SetColumnSpan(btnPanel, 4);

            formPanel.Controls.Add(table);

            // Grid
            dgvClientes = CreateDataGridView();
            dgvClientes.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Visible = false },
                new DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Nombre" },
                new DataGridViewTextBoxColumn { Name = "Apellido", HeaderText = "Apellido" },
                new DataGridViewTextBoxColumn { Name = "Telefono", HeaderText = "Teléfono" },
                new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email" },
                new DataGridViewTextBoxColumn { Name = "Documento", HeaderText = "N° Documento" },
                new DataGridViewTextBoxColumn { Name = "TipoDoc", HeaderText = "Tipo Doc" },
                new DataGridViewTextBoxColumn { Name = "FechaNac", HeaderText = "Fecha Nac." },
                new DataGridViewTextBoxColumn { Name = "Direccion", HeaderText = "Dirección" }
            );

            // Add controls (order matters for Dock layout: last added is rendered first)
            pageClientes.Controls.Add(dgvClientes);
            pageClientes.Controls.Add(formPanel);
            pageClientes.Controls.Add(lblTitulo);

            // Events
            btnClienteAgregar.Click += async (_, _) => await AgregarCliente();
            btnClienteActualizar.Click += async (_, _) => await ActualizarClienteAsync();
            btnClienteLimpiar.Click += (_, _) => LimpiarFormCliente();
            dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;
        }

        private async Task CargarClientes()
        {
            try
            {
                var clientes = await _customerService.ObtenerClientes();
                dgvClientes.Rows.Clear();
                foreach (var c in clientes)
                {
                    dgvClientes.Rows.Add(
                        c.Id, c.Nombre, c.Apellido, c.Telefono, c.Email,
                        c.NumeroDocumento, c.TipoDocumento.ToString(),
                        c.FechaNacimiento.ToString("dd/MM/yyyy"), c.Direccion
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AgregarCliente()
        {
            try
            {
                var cliente = new Cliente(
                    Guid.NewGuid(),
                    txtClienteNombre.Text.Trim(),
                    txtClienteApellido.Text.Trim(),
                    txtClienteTelefono.Text.Trim(),
                    txtClienteEmail.Text.Trim(),
                    DateOnly.FromDateTime(dtpClienteFechaNac.Value),
                    txtClienteDocumento.Text.Trim(),
                    (TipoDocumento)cmbClienteTipoDoc.SelectedItem!,
                    txtClienteDireccion.Text.Trim()
                );

                if (await _customerService.RegistrarCliente(cliente))
                {
                    MessageBox.Show("Cliente registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormCliente();
                    await CargarClientes();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el cliente. Verifique que los datos sean válidos y que el documento no esté duplicado.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ActualizarClienteAsync()
        {
            if (_selectedClienteId is null) return;

            try
            {
                var cliente = new Cliente(
                    _selectedClienteId.Value,
                    txtClienteNombre.Text.Trim(),
                    txtClienteApellido.Text.Trim(),
                    txtClienteTelefono.Text.Trim(),
                    txtClienteEmail.Text.Trim(),
                    DateOnly.FromDateTime(dtpClienteFechaNac.Value),
                    txtClienteDocumento.Text.Trim(),
                    (TipoDocumento)cmbClienteTipoDoc.SelectedItem!,
                    txtClienteDireccion.Text.Trim()
                );

                if (await _customerService.ActualizarCliente(cliente))
                {
                    MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormCliente();
                    await CargarClientes();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el cliente. Verifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormCliente()
        {
            txtClienteNombre.Clear();
            txtClienteApellido.Clear();
            txtClienteTelefono.Clear();
            txtClienteEmail.Clear();
            txtClienteDocumento.Clear();
            txtClienteDireccion.Clear();
            cmbClienteTipoDoc.SelectedIndex = 0;
            dtpClienteFechaNac.Value = DateTime.Today;
            _selectedClienteId = null;
            btnClienteAgregar.Enabled = true;
            btnClienteActualizar.Enabled = false;
            dgvClientes.ClearSelection();
        }

        private void DgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
                return;

            var row = dgvClientes.SelectedRows[0];
            if (row.Cells["Id"].Value is not Guid id)
                return;

            _selectedClienteId = id;
            txtClienteNombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
            txtClienteApellido.Text = row.Cells["Apellido"].Value?.ToString() ?? "";
            txtClienteTelefono.Text = row.Cells["Telefono"].Value?.ToString() ?? "";
            txtClienteEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            txtClienteDocumento.Text = row.Cells["Documento"].Value?.ToString() ?? "";
            txtClienteDireccion.Text = row.Cells["Direccion"].Value?.ToString() ?? "";

            var tipoDocStr = row.Cells["TipoDoc"].Value?.ToString() ?? "";
            if (Enum.TryParse<TipoDocumento>(tipoDocStr, out var tipoDoc))
                cmbClienteTipoDoc.SelectedItem = tipoDoc;

            var fechaStr = row.Cells["FechaNac"].Value?.ToString() ?? "";
            if (DateOnly.TryParseExact(fechaStr, "dd/MM/yyyy", out var fechaNac))
            {
                var fechaDt = fechaNac.ToDateTime(TimeOnly.MinValue);
                if (fechaDt >= dtpClienteFechaNac.MinDate && fechaDt <= dtpClienteFechaNac.MaxDate)
                    dtpClienteFechaNac.Value = fechaDt;
            }

            btnClienteAgregar.Enabled = false;
            btnClienteActualizar.Enabled = true;
        }
    }
}
