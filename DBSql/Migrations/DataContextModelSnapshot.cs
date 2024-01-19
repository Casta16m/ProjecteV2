﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjecteV2.ApiSql;

#nullable disable

namespace DBSql.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtistaGrup", b =>
                {
                    b.Property<string>("GrupsNomGrup")
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("artistesNomArtista")
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("GrupsNomGrup", "artistesNomArtista");

                    b.HasIndex("artistesNomArtista");

                    b.ToTable("ArtistaGrup");
                });

            modelBuilder.Entity("ExtensioSong", b =>
                {
                    b.Property<string>("extensioNomExtensio")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("songsUID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("extensioNomExtensio", "songsUID");

                    b.HasIndex("songsUID");

                    b.ToTable("ExtensioSong");
                });

            modelBuilder.Entity("LlistaSong", b =>
                {
                    b.Property<string>("songsUID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("llistaNom")
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("llistaID_MAC")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("songsUID", "llistaNom", "llistaID_MAC");

                    b.HasIndex("llistaNom", "llistaID_MAC");

                    b.ToTable("LlistaSong");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Album", b =>
                {
                    b.Property<string>("NomAlbum")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("UIDSong")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime2");

                    b.HasKey("NomAlbum", "UIDSong", "data");

                    b.HasIndex("UIDSong");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Artista", b =>
                {
                    b.Property<string>("NomArtista")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("AnyNaixement")
                        .HasColumnType("int");

                    b.HasKey("NomArtista");

                    b.ToTable("Artistes");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Extensio", b =>
                {
                    b.Property<string>("NomExtensio")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("NomExtensio");

                    b.ToTable("Extensio");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Grup", b =>
                {
                    b.Property<string>("NomGrup")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("NomGrup");

                    b.ToTable("Grups");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Instrument", b =>
                {
                    b.Property<string>("Nom")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Nom");

                    b.ToTable("Instrument");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Llista", b =>
                {
                    b.Property<string>("Nom")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ID_MAC")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Dispositiu")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Nom", "ID_MAC");

                    b.ToTable("Llista");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Participa", b =>
                {
                    b.Property<string>("UID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NomArtista")
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("NomGrup")
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("NomInstrument")
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("UID", "NomArtista", "NomGrup", "NomInstrument");

                    b.HasIndex("NomArtista");

                    b.HasIndex("NomGrup");

                    b.HasIndex("NomInstrument");

                    b.ToTable("Participa");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Song", b =>
                {
                    b.Property<string>("UID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Genere")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("NomSong")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<string>("SongOriginal")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("data")
                        .HasColumnType("datetime2");

                    b.HasKey("UID");

                    b.HasIndex("SongOriginal");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("ArtistaGrup", b =>
                {
                    b.HasOne("ProjecteV2.ApiSql.Grup", null)
                        .WithMany()
                        .HasForeignKey("GrupsNomGrup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjecteV2.ApiSql.Artista", null)
                        .WithMany()
                        .HasForeignKey("artistesNomArtista")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExtensioSong", b =>
                {
                    b.HasOne("ProjecteV2.ApiSql.Extensio", null)
                        .WithMany()
                        .HasForeignKey("extensioNomExtensio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjecteV2.ApiSql.Song", null)
                        .WithMany()
                        .HasForeignKey("songsUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LlistaSong", b =>
                {
                    b.HasOne("ProjecteV2.ApiSql.Song", null)
                        .WithMany()
                        .HasForeignKey("songsUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjecteV2.ApiSql.Llista", null)
                        .WithMany()
                        .HasForeignKey("llistaNom", "llistaID_MAC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Album", b =>
                {
                    b.HasOne("ProjecteV2.ApiSql.Song", "SongObj")
                        .WithMany("album")
                        .HasForeignKey("UIDSong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SongObj");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Participa", b =>
                {
                    b.HasOne("ProjecteV2.ApiSql.Artista", "ArtistaObj")
                        .WithMany("participa")
                        .HasForeignKey("NomArtista")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjecteV2.ApiSql.Grup", "GrupObj")
                        .WithMany("participa")
                        .HasForeignKey("NomGrup")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjecteV2.ApiSql.Instrument", "InstrumentObj")
                        .WithMany("participa")
                        .HasForeignKey("NomInstrument")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjecteV2.ApiSql.Song", "SongObj")
                        .WithMany("participa")
                        .HasForeignKey("UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtistaObj");

                    b.Navigation("GrupObj");

                    b.Navigation("InstrumentObj");

                    b.Navigation("SongObj");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Song", b =>
                {
                    b.HasOne("ProjecteV2.ApiSql.Song", "SongObj")
                        .WithMany("songs")
                        .HasForeignKey("SongOriginal")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("SongObj");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Artista", b =>
                {
                    b.Navigation("participa");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Grup", b =>
                {
                    b.Navigation("participa");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Instrument", b =>
                {
                    b.Navigation("participa");
                });

            modelBuilder.Entity("ProjecteV2.ApiSql.Song", b =>
                {
                    b.Navigation("album");

                    b.Navigation("participa");

                    b.Navigation("songs");
                });
#pragma warning restore 612, 618
        }
    }
}
