CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE owners (
    id uuid NOT NULL,
    created_on timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_on timestamp without time zone NULL,
    updated_by uuid NULL,
    deleted boolean NOT NULL,
    username text NOT NULL,
    "PasswordHash" text NOT NULL,
    password text NOT NULL,
    email text NOT NULL,
    phone text NOT NULL,
    CONSTRAINT "PK_owners" PRIMARY KEY (id)
);

CREATE TABLE properties_owners (
    id uuid NOT NULL,
    created_on timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_on timestamp without time zone NULL,
    updated_by uuid NULL,
    deleted boolean NOT NULL,
    name text NOT NULL,
    email text NOT NULL,
    phone text NOT NULL,
    address text NOT NULL,
    CONSTRAINT "PK_properties_owners" PRIMARY KEY (id)
);

CREATE TABLE tenants (
    id uuid NOT NULL,
    created_on timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_on timestamp without time zone NULL,
    updated_by uuid NULL,
    deleted boolean NOT NULL,
    name text NOT NULL,
    email text NOT NULL,
    phone text NOT NULL,
    address text NOT NULL,
    CONSTRAINT "PK_tenants" PRIMARY KEY (id)
);

CREATE TABLE properties (
    id uuid NOT NULL,
    created_on timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_on timestamp without time zone NULL,
    updated_by uuid NULL,
    deleted boolean NOT NULL,
    property_owner_id uuid NOT NULL,
    name text NOT NULL,
    address text NOT NULL,
    rent text NOT NULL,
    area text NOT NULL,
    CONSTRAINT "PK_properties" PRIMARY KEY (id),
    CONSTRAINT "FK_properties_properties_owners_property_owner_id" FOREIGN KEY (property_owner_id) REFERENCES properties_owners (id) ON DELETE CASCADE
);

CREATE TABLE assigned_properties (
    id uuid NOT NULL,
    created_on timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_on timestamp without time zone NULL,
    updated_by uuid NULL,
    deleted boolean NOT NULL,
    tenant_id uuid NOT NULL,
    property_id uuid NOT NULL,
    rent text NOT NULL,
    "DateFrom" timestamp without time zone NOT NULL,
    "DateTo" timestamp without time zone NOT NULL,
    "RentStartDate" timestamp without time zone NOT NULL,
    "RentDocumentFilePath" text NULL,
    CONSTRAINT "PK_assigned_properties" PRIMARY KEY (id),
    CONSTRAINT "FK_assigned_properties_properties_property_id" FOREIGN KEY (property_id) REFERENCES properties (id) ON DELETE CASCADE,
    CONSTRAINT "FK_assigned_properties_tenants_tenant_id" FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE assigned_property_histories (
    id uuid NOT NULL,
    created_on timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_on timestamp without time zone NULL,
    updated_by uuid NULL,
    deleted boolean NOT NULL,
    tenant_id uuid NOT NULL,
    property_id uuid NOT NULL,
    rent text NOT NULL,
    date_from timestamp without time zone NOT NULL,
    date_to timestamp without time zone NULL,
    "RentStartDate" timestamp without time zone NOT NULL,
    "RentDocumentFilePath" text NULL,
    CONSTRAINT "PK_assigned_property_histories" PRIMARY KEY (id),
    CONSTRAINT "FK_assigned_property_histories_properties_property_id" FOREIGN KEY (property_id) REFERENCES properties (id) ON DELETE CASCADE,
    CONSTRAINT "FK_assigned_property_histories_tenants_tenant_id" FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE payments (
    id uuid NOT NULL,
    created_on timestamp without time zone NOT NULL,
    created_by uuid NOT NULL,
    updated_on timestamp without time zone NULL,
    updated_by uuid NULL,
    deleted boolean NOT NULL,
    property_id uuid NOT NULL,
    property_owner_id uuid NULL,
    tenant_id uuid NULL,
    amount text NOT NULL,
    credit boolean NOT NULL,
    debit boolean NOT NULL,
    CONSTRAINT "PK_payments" PRIMARY KEY (id),
    CONSTRAINT "FK_payments_properties_property_id" FOREIGN KEY (property_id) REFERENCES properties (id) ON DELETE CASCADE,
    CONSTRAINT "FK_payments_properties_owners_property_owner_id" FOREIGN KEY (property_owner_id) REFERENCES properties_owners (id) ON DELETE RESTRICT,
    CONSTRAINT "FK_payments_tenants_tenant_id" FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE RESTRICT
);

CREATE INDEX "IX_assigned_properties_property_id" ON assigned_properties (property_id);

CREATE INDEX "IX_assigned_properties_tenant_id" ON assigned_properties (tenant_id);

CREATE INDEX "IX_assigned_property_histories_property_id" ON assigned_property_histories (property_id);

CREATE INDEX "IX_assigned_property_histories_tenant_id" ON assigned_property_histories (tenant_id);

CREATE INDEX "IX_payments_property_id" ON payments (property_id);

CREATE INDEX "IX_payments_property_owner_id" ON payments (property_owner_id);

CREATE INDEX "IX_payments_tenant_id" ON payments (tenant_id);

CREATE INDEX "IX_properties_property_owner_id" ON properties (property_owner_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190704075359_InitialCreate', '2.2.4-servicing-10062');

