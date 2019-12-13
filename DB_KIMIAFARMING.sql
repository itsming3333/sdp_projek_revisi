ALTER TABLE TRANSAKSI DROP CONSTRAINT ID_MEMBER CASCADE;
ALTER TABLE PEGAWAI DROP CONSTRAINT ID_JABATAN CASCADE;
ALTER TABLE SHIFT_SPESIALIS DROP CONSTRAINT ID_PEGAWAI CASCADE;
ALTER TABLE SHIFT_SPESIALIS DROP CONSTRAINT ID_PERAWATAN CASCADE;
ALTER TABLE SHIFT_SPESIALIS DROP CONSTRAINT ID_RUANG CASCADE;
ALTER TABLE DTRANS_RUANG DROP CONSTRAINT ID_RUANG_TRANS CASCADE;
ALTER TABLE DTRANS_RUANG DROP CONSTRAINT ID_TRANS_RUANG CASCADE;
ALTER TABLE DTRANS_PEMBAYARAN DROP CONSTRAINT ID_PEMBAYARAN CASCADE;
ALTER TABLE DTRANS_PEMBAYARAN DROP CONSTRAINT ID_TRANS CASCADE;
ALTER TABLE DTRANS_SUPPLY DROP CONSTRAINT ID_SUPPLY CASCADE;
ALTER TABLE DTRANS_SUPPLY DROP CONSTRAINT ID_TRANS_SUPPLY CASCADE;
ALTER TABLE DONOR DROP CONSTRAINT ID_MEMBER_DONOR CASCADE;
ALTER TABLE DONOR DROP CONSTRAINT ID_SUPPLY_DONOR CASCADE;
ALTER TABLE DTRANS_PERAWATAN_INAP DROP CONSTRAINT ID_TRANS_CHECKUP CASCADE;
ALTER TABLE DTRANS_PERAWATAN_INAP DROP CONSTRAINT ID_PERAWATAN_CHECKUP CASCADE;
ALTER TABLE DTRANS_PERAWATAN_INAP DROP CONSTRAINT ID_PEGAWAI_CHECKUP CASCADE;
ALTER TABLE DTRANS_JALAN DROP CONSTRAINT ID_TRANS_JALAN CASCADE;
ALTER TABLE DTRANS_JALAN DROP CONSTRAINT ID_PERAWATAN_JALAN CASCADE;
ALTER TABLE DTRANS_JALAN DROP CONSTRAINT ID_PEGAWAI_JALAN CASCADE;
ALTER TABLE ISI_STOK DROP CONSTRAINT ID_PRODUSEN CASCADE;
ALTER TABLE DTRANS_STOK DROP CONSTRAINT ID_TRANS_STOK CASCADE;
ALTER TABLE DTRANS_STOK DROP CONSTRAINT ID_SUPPLY_STOK CASCADE;

DROP TABLE MEMBER CASCADE CONSTRAINTS;
DROP TABLE SUPPLY CASCADE CONSTRAINTS;
DROP TABLE TRANSAKSI CASCADE CONSTRAINTS;
DROP TABLE PEGAWAI CASCADE CONSTRAINTS;
DROP TABLE JABATAN CASCADE CONSTRAINTS;
DROP TABLE PERAWATAN CASCADE CONSTRAINTS;
DROP TABLE PEMBAYARAN CASCADE CONSTRAINTS;
DROP TABLE table_1 CASCADE CONSTRAINTS;
DROP TABLE PRODUSEN CASCADE CONSTRAINTS;
DROP TABLE RUANG CASCADE CONSTRAINTS;
DROP TABLE SHIFT_SPESIALIS CASCADE CONSTRAINTS;
DROP TABLE DTRANS_RUANG CASCADE CONSTRAINTS;
DROP TABLE DTRANS_PEMBAYARAN CASCADE CONSTRAINTS;
DROP TABLE DTRANS_SUPPLY CASCADE CONSTRAINTS;
DROP TABLE DONOR CASCADE CONSTRAINTS;
DROP TABLE table_2 CASCADE CONSTRAINTS;
DROP TABLE SURAT_TUGAS CASCADE CONSTRAINTS;
DROP TABLE DTRANS_PERAWATAN_INAP CASCADE CONSTRAINTS;
DROP TABLE table_3 CASCADE CONSTRAINTS;
DROP TABLE DTRANS_JALAN CASCADE CONSTRAINTS;
DROP TABLE ISI_STOK CASCADE CONSTRAINTS;
DROP TABLE DTRANS_STOK CASCADE CONSTRAINTS;
DROP TABLE JENIS_PERAWATAN CASCADE CONSTRAINTS;

CREATE TABLE MEMBER (
ID_MEMBER VARCHAR2(5) NOT NULL,
NAMA_MEMBER VARCHAR2(50) NOT NULL,
TGLLAHIR_MEMBER DATE NOT NULL,
ALAMAT_MEMBER VARCHAR2(50) NOT NULL,
TELP_MEMBER VARCHAR2(12) NOT NULL,
GOL_DARAH VARCHAR2(2) NOT NULL,
PEKERJAAN_MEMBER VARCHAR2(30) NOT NULL,
AGAMA VARCHAR2(20) NOT NULL,
JK_MEMBER VARCHAR2(1) NOT NULL,
NIK VARCHAR2(16) NOT NULL,
BERAT_MEMBER NUMBER(3,0) NULL,
TINGGI_MEMBER NUMBER(3,0) NULL,
PRIMARY KEY (ID_MEMBER) 
);
CREATE TABLE SUPPLY (
ID_SUPPLY VARCHAR2(5) NOT NULL,
NAMA_SUPPLY VARCHAR2(30) NOT NULL,
DESKRIPSI_SUPPLY VARCHAR2(300) NOT NULL,
HARGA_SUPPLY NUMBER(12,0) NOT NULL,
JENIS_SUPPLY VARCHAR2(30) NOT NULL,
SATUAN VARCHAR2(20) NOT NULL,
PRIMARY KEY (ID_SUPPLY) 
);
CREATE TABLE DSUPPLY(
KODE_BARANG VARCHAR2(30) NOT NULL,
ID_SUPPLY VARCHAR2(5) NOT NULL,
EXPIRED DATE NULL	
);
CREATE TABLE TRANSAKSI (
ID_TRANS VARCHAR2(12) NOT NULL,
TGL_MASUK DATE NOT NULL,
TGL_KELUAR DATE NULL,
KONDISI_KELUAR VARCHAR2(100) NULL,
TOTAL_TRANSAKSI NUMBER(12,0) NOT NULL,
ID_MEMBER VARCHAR2(5) NOT NULL,
STATUS_PELUNASAN VARCHAR2(1) NULL,
DIAGNOSA_MASUK VARCHAR2(255) NOT NULL,
ALERGI VARCHAR2(255) NULL,
NAMA_WALI VARCHAR2(50) NOT NULL,
TELP_WALI VARCHAR2(12) NOT NULL,
RELASI_WALI VARCHAR2(30) NOT NULL,
JENIS_RAWAT VARCHAR2(30) NOT NULL,
PRIMARY KEY (ID_TRANS) 
);
CREATE TABLE PEGAWAI (
ID_PEGAWAI VARCHAR2(5) NOT NULL,
PASSWORD_PEGAWAI VARCHAR(30) NOT NULL,
NAMA_PEGAWAI VARCHAR2(50) NOT NULL,
GOL_DARAH_PEGAWAI VARCHAR2(2) NOT NULL,
TELP_PEGAWAI VARCHAR2(12) NOT NULL,
TGLLAHIR_PEGAWAI DATE NOT NULL,
ALAMAT_PEGAWAI VARCHAR2(50) NOT NULL,
NIK VARCHAR2(16) NOT NULL,
ID_JABATAN VARCHAR2(5) NOT NULL,
RUANG_KANTOR VARCHAR2(5) NOT NULL,
AGAMA_PEGAWAI VARCHAR2(30) NOT NULL,
JK_PEGAWAI VARCHAR2(1) NOT NULL,
NPWP_PEGAWAI VARCHAR2(15) NOT NULL,
WALI_PEGAWAI VARCHAR2(30) NULL,
KONTAK_WALI_PEGAWAI VARCHAR2(12) NULL,
PRIMARY KEY (ID_PEGAWAI) 
);
CREATE TABLE JABATAN (
ID_JABATAN VARCHAR2(3) NOT NULL,
NAMA_JABATAN VARCHAR2(30) NOT NULL,
GAJI_JABATAN NUMBER(12,0) NOT NULL,
PRIMARY KEY (ID_JABATAN) 
);
CREATE TABLE PERAWATAN (
ID_PERAWATAN VARCHAR2(5) NOT NULL,
NAMA_PERAWATAN VARCHAR2(30) NOT NULL,
DESKRIPSI_PERAWATAN VARCHAR2(255) NOT NULL,
HARGA_PERAWATAN NUMBER(12,0) NOT NULL,
JENIS_PERAWATAN VARCHAR2(30) NOT NULL,
PRIMARY KEY (ID_PERAWATAN) 
);
CREATE TABLE PEMBAYARAN (
ID_PEMBAYARAN VARCHAR2(3) NOT NULL,
NAMA_PEMBAYARAN VARCHAR2(30) NOT NULL,
PRIMARY KEY (ID_PEMBAYARAN) 
);
CREATE TABLE PRODUSEN (
ID_PRODUSEN VARCHAR2(5) NOT NULL,
NAMA_PRODUSEN VARCHAR2(30) NOT NULL,
PRIMARY KEY (ID_PRODUSEN) 
);
CREATE TABLE RUANG (
ID_RUANG VARCHAR2(5) NOT NULL,
NOMOR_RUANG VARCHAR2(4) NOT NULL,
JENIS_RUANG VARCHAR2(30) NOT NULL,
HARGA_RUANG NUMBER(12,0) NULL,
STATUS_RUANG VARCHAR2(6) NOT NULL,
NAMA_RUANG VARCHAR2(30) NOT NULL,
PRIMARY KEY (ID_RUANG) 
);
CREATE TABLE SHIFT_SPESIALIS (
ID_PEGAWAI VARCHAR2(5) NOT NULL,
ID_PERAWATAN VARCHAR2(5) NOT NULL,
WAKTU_MULAI DATE NOT NULL,
WAKTU_SELESAI DATE NOT NULL,
ID_RUANG VARCHAR2(5) NOT NULL,
ID_SHIFT VARCHAR2(8) NOT NULL,
HARGA_SHIFT NUMBER(12,0) NOT NULL,
HARI_SHIFT VARCHAR2(12) NOT NULL,
PRIMARY KEY (ID_SHIFT,ID_PEGAWAI,ID_PERAWATAN) 
);
CREATE TABLE DTRANS_RUANG (
ID_RUANG VARCHAR2(5) NOT NULL,
ID_TRANS VARCHAR2(12) NOT NULL,
TOTAL_HARI NUMBER(3,0) NOT NULL,
SUBTOTAL NUMBER(12,0) NOT NULL,
LUNAS_RUANG VARCHAR2(1) NOT NULL,
PRIMARY KEY (ID_RUANG, ID_TRANS) 
);
CREATE TABLE DTRANS_PEMBAYARAN (
ID_PEMBAYARAN VARCHAR2(3) NOT NULL,
ID_TRANS VARCHAR2(12) NOT NULL,
JUMLAH_PEMBAYARAN NUMBER(12,0) NOT NULL,
PRIMARY KEY (ID_TRANS, ID_PEMBAYARAN) 
);
CREATE TABLE DTRANS_SUPPLY (
ID_SUPPLY VARCHAR2(5) NOT NULL,
ID_TRANS VARCHAR2(12) NOT NULL,
ID_PEGAWAI VARCHAR(5) NOT NULL,
JUMLAH NUMBER(10,0) NOT NULL,
SUBTOTAL NUMBER(12,0) NOT NULL,
STATUS_AMBIL VARCHAR2(1) NOT NULL,
CTR_SUPPLY NUMBER(3,0) NOT NULL,
LUNAS_SUPPLY VARCHAR2(1) NOT NULL,
PRIMARY KEY (ID_TRANS,ID_SUPPLY,CTR_SUPPLY) 
);
CREATE TABLE DONOR (
ID_DONOR VARCHAR2(12) NOT NULL,
ID_MEMBER VARCHAR2(5) NOT NULL,
ID_SUPPLY VARCHAR2(5) NOT NULL,
TENSI_MEMBER VARCHAR2(10) NULL,
SUHU_MEMBER NUMBER(3,0) NOT NULL,
REAKSI_DONOR VARCHAR2(255) NOT NULL,
KETERANGAN_DONOR VARCHAR2(255) NULL,
TGL_DONOR DATE NOT NULL,
NADI NUMBER(3,0) NOT NULL,
RHESUS VARCHAR2(1) NOT NULL,
ID_PETUGAS_DONOR VARCHAR2(5) NULL,
PRIMARY KEY (ID_DONOR) 
);
CREATE TABLE DTRANS_PERAWATAN_INAP (
ID_TRANS VARCHAR2(12) NOT NULL,
ID_PERAWATAN VARCHAR2(5) NOT NULL,
ID_PEGAWAI VARCHAR2(5) NOT NULL,
CTR_CHECKUP NUMBER(3,0) NOT NULL,
KETERANGAN_CHECKUP VARCHAR2(255) NOT NULL,
KELUHAN_TAMBAHAN VARCHAR2(255) NULL,
TINDAK_LANJUT VARCHAR2(255) NOT NULL,
TGL_RAWAT DATE NOT NULL,
LUNAS_RAWAT VARCHAR2(1) NOT NULL,
PRIMARY KEY (ID_TRANS, CTR_CHECKUP)
);
CREATE TABLE DTRANS_JALAN (
ID_TRANS VARCHAR2(12) NOT NULL,
ID_PERAWATAN VARCHAR2(5) NOT NULL,
CTR_JALAN NUMBER(3,0) NOT NULL,
KONDISI_JALAN VARCHAR2(255) NULL,
ID_PEGAWAI VARCHAR2(5) NOT NULL,
KELUHAN_TAMBAHAN VARCHAR2(255) NULL,
SARAN_LANJUTAN VARCHAR2(255) NOT NULL,
PRIMARY KEY (ID_TRANS, CTR_JALAN) 
);
CREATE TABLE ISI_STOK (
ID_TRANS_STOK VARCHAR2(12) NOT NULL,
ID_PRODUSEN VARCHAR2(255) NOT NULL,
TGL_TRANS_STOK DATE NOT NULL,
TANDA_TERIMA VARCHAR2(50) NOT NULL,
PRIMARY KEY (ID_TRANS_STOK) 
);
CREATE TABLE DTRANS_STOK (
ID_TRANS_STOK VARCHAR2(12) NOT NULL,
ID_SUPPLY VARCHAR2(5) NOT NULL,
TOTAL_STOK NUMBER(10,0) NOT NULL,
JML_BELI NUMBER NOT NULL,
PRIMARY KEY (ID_TRANS_STOK, ID_SUPPLY) 
);

ALTER TABLE TRANSAKSI ADD CONSTRAINT ID_MEMBER FOREIGN KEY (ID_MEMBER) REFERENCES MEMBER (ID_MEMBER);
ALTER TABLE PEGAWAI ADD CONSTRAINT ID_JABATAN FOREIGN KEY (ID_JABATAN) REFERENCES JABATAN (ID_JABATAN);
ALTER TABLE DTRANS_RUANG ADD CONSTRAINT ID_RUANG_TRANS FOREIGN KEY (ID_RUANG) REFERENCES RUANG (ID_RUANG);
ALTER TABLE DTRANS_RUANG ADD CONSTRAINT ID_TRANS_RUANG FOREIGN KEY (ID_TRANS) REFERENCES TRANSAKSI (ID_TRANS);
ALTER TABLE DTRANS_PEMBAYARAN ADD CONSTRAINT ID_PEMBAYARAN FOREIGN KEY (ID_PEMBAYARAN) REFERENCES PEMBAYARAN (ID_PEMBAYARAN);
ALTER TABLE DTRANS_PEMBAYARAN ADD CONSTRAINT ID_TRANS FOREIGN KEY (ID_TRANS) REFERENCES TRANSAKSI (ID_TRANS);
ALTER TABLE DTRANS_SUPPLY ADD CONSTRAINT ID_SUPPLY FOREIGN KEY (ID_SUPPLY) REFERENCES SUPPLY (ID_SUPPLY);
ALTER TABLE DTRANS_SUPPLY ADD CONSTRAINT ID_TRANS_SUPPLY FOREIGN KEY (ID_TRANS) REFERENCES TRANSAKSI (ID_TRANS);
ALTER TABLE DONOR ADD CONSTRAINT ID_MEMBER_DONOR FOREIGN KEY (ID_MEMBER) REFERENCES MEMBER (ID_MEMBER);
ALTER TABLE DONOR ADD CONSTRAINT ID_SUPPLY_DONOR FOREIGN KEY (ID_SUPPLY) REFERENCES SUPPLY (ID_SUPPLY);
ALTER TABLE DTRANS_PERAWATAN_INAP ADD CONSTRAINT ID_TRANS_CHECKUP FOREIGN KEY (ID_TRANS) REFERENCES TRANSAKSI (ID_TRANS);
ALTER TABLE DTRANS_JALAN ADD CONSTRAINT ID_TRANS_JALAN FOREIGN KEY (ID_TRANS) REFERENCES TRANSAKSI (ID_TRANS);
ALTER TABLE ISI_STOK ADD CONSTRAINT ID_PRODUSEN FOREIGN KEY (ID_PRODUSEN) REFERENCES PRODUSEN (ID_PRODUSEN);
ALTER TABLE DTRANS_STOK ADD CONSTRAINT ID_TRANS_STOK FOREIGN KEY (ID_TRANS_STOK) REFERENCES ISI_STOK (ID_TRANS_STOK);

--FUNCTION/PROCEDURE/TRIGGER--
--1
create or replace function AUTO_GEN_ID_MEMBER(a varchar2)
return varchar2
is
    hasil varchar2(30);
begin
    select LPAD(count(ID_MEMBER),3,'0') into hasil from MEMBER where ID_MEMBER like '%'||a||'%';
    return hasil;
end;
/
show err;

--2
create or replace function AUTO_GEN_ID_TRANS(a varchar2)
return varchar2
is
    hasil varchar2(30);
begin
    select LPAD(count(ID_TRANS),4,'0') into hasil from TRANSAKSI where ID_TRANS like '%'||a||'%';
    return hasil;
end;
/
show err;

--3
create or replace function AUTO_GEN_ID_DONOR(a varchar2)
return varchar2
is
    hasil varchar2(30);
begin
    select LPAD(count(ID_DONOR),3,'0') into hasil from DONOR where ID_DONOR like '%'||a||'%';
    return hasil;
end;
/
show err;

--4
create or replace function AUTO_GEN_ID_PEGAWAI(a varchar2)
return varchar2
is
    hasil varchar2(30);
begin
    select LPAD(count(ID_PEGAWAI),3,'0') into hasil from PEGAWAI where ID_PEGAWAI like '%'||a||'%';
    return hasil;
end;
/
show err;
--5
create or replace function AUTO_GEN_ID_STOK(a varchar2)
return varchar2
is
    hasil varchar2(30);
begin
    select LPAD(count(ID_TRANS_STOK),3,'0') into hasil from ISI_STOK where ID_TRANS_STOK like '%'||a||'%';
    return hasil;
end;
/
show err;


