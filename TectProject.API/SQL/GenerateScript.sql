CREATE DATABASE BpkbManagement;
GO

USE BpkbManagement;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ms_storage_location') AND type in (N'U'))
BEGIN
    CREATE TABLE ms_storage_location (
        location_id VARCHAR(10) PRIMARY KEY,
        location_name VARCHAR(100) NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ms_user') AND type in (N'U'))
BEGIN
    CREATE TABLE ms_user (
        user_id BIGINT PRIMARY KEY IDENTITY(1,1),
        user_name VARCHAR(20) NOT NULL,
        password VARCHAR(50) NOT NULL,
        is_active BIT NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'tr_bpkb') AND type in (N'U'))
BEGIN
    CREATE TABLE tr_bpkb (
        agreement_number VARCHAR(100) PRIMARY KEY,
        bpkb_no VARCHAR(100) NOT NULL,
        branch_id VARCHAR(10) NOT NULL,
        bpkb_date DATETIME NOT NULL,
        faktur_no VARCHAR(100) NOT NULL,
        faktur_date DATETIME NOT NULL,
        location_id VARCHAR(10) NOT NULL,
        police_no VARCHAR(20) NOT NULL,
        bpkb_date_in DATETIME NOT NULL,
        created_by VARCHAR(20) NOT NULL,
        created_on DATETIME NOT NULL,
        last_updated_by VARCHAR(20) NOT NULL,
        last_updated_on DATETIME NOT NULL,
        CONSTRAINT FK_tr_bpkb_ms_storage_location FOREIGN KEY (location_id) 
            REFERENCES ms_storage_location(location_id)
    );
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'tr_bpkb') AND name = N'IX_tr_bpkb_location_id')
BEGIN
    CREATE INDEX IX_tr_bpkb_location_id ON tr_bpkb(location_id);
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'ms_user') AND name = N'IX_ms_user_user_name')
BEGIN
    CREATE INDEX IX_ms_user_user_name ON ms_user(user_name);
END
