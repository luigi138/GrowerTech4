using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrowerTech_MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AGRICULTORES",
                columns: table => new
                {
                    AgricultorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ESCALA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ENDERECO = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AGRICULTORES", x => x.AgricultorId);
                });

            migrationBuilder.CreateTable(
                name: "SENSORES",
                columns: table => new
                {
                    SensorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TIPO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    LOCALIZACAO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENSORES", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "DADOS_CLIMATICOS",
                columns: table => new
                {
                    DadoClimaticoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SENSOR_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TEMPERATURA = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    UMIDADE = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DADOS_CLIMATICOS", x => x.DadoClimaticoId);
                    table.ForeignKey(
                        name: "FK_DADOS_CLIMATICOS_SENSORES_SENSOR_ID",
                        column: x => x.SENSOR_ID,
                        principalTable: "SENSORES",
                        principalColumn: "SensorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DADOS_CLIMATICOS_SENSOR_ID",
                table: "DADOS_CLIMATICOS",
                column: "SENSOR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AGRICULTORES");

            migrationBuilder.DropTable(
                name: "DADOS_CLIMATICOS");

            migrationBuilder.DropTable(
                name: "SENSORES");
        }
    }
}
