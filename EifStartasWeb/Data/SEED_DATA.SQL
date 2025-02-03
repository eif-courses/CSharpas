-- Seed Roles
INSERT OR IGNORE INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES
                                                                                   ('550e8400-e29b-41d4-a716-446655440000', 'Admin', 'ADMIN', 'concurrency_stamp_1'),
                                                                                   ('550e8400-e29b-41d4-a716-446655440001', 'Reviewer', 'REVIEWER', 'concurrency_stamp_2'),
                                                                                   ('550e8400-e29b-41d4-a716-446655440002', 'Student', 'STUDENT', 'concurrency_stamp_3'),
                                                                                   ('550e8400-e29b-41d4-a716-446655440003', 'Supervisor', 'SUPERVISOR', 'concurrency_stamp_4'),
                                                                                   ('550e8400-e29b-41d4-a716-446655440004', 'Department', 'DEPARTMENT', 'concurrency_stamp_5');

-- Create a unique identifier for the admin user
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, SecurityStamp, PasswordHash) VALUES
    ('550e8400-e29b-41d4-a716-446655440005', 'admin@viko.lt', 'ADMIN@VIKO.LT', 'admin@viko.lt', 'ADMIN@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp',
     'AQAAAAIAAYagAAAAEDqt1iT9qTgfl9FhaRjTDW569sc7/90KJM7dvmL8jrBdSk1N8WnsFQRmXQ5eS06Sww==');

-- Seed User-Role association for Admin
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
    ('550e8400-e29b-41d4-a716-446655440005', '550e8400-e29b-41d4-a716-446655440000');

-- Seed 20 Reviewer users
BEGIN TRANSACTION;

INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, SecurityStamp, PasswordHash) VALUES
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440006', 'PS1', 'PS1', 'PS1@viko.lt', 'PS1@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_1', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440007', 'PS2', 'PS2', 'PS2@viko.lt', 'PS2@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_2', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440008', 'PS3', 'PS3', 'PS3@viko.lt', 'PS3@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_3', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440009', 'PS4', 'PS4', 'PS4@viko.lt', 'PS4@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_4', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-44665544000A', 'PS5', 'PS5', 'PS5@viko.lt', 'PS5@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_5', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-44665544000B', 'PS6', 'PS6', 'PS6@viko.lt', 'PS6@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_6', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-44665544000C', 'PS7', 'PS7', 'PS7@viko.lt', 'PS7@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_7', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-44665544000D', 'PS8', 'PS8', 'PS8@viko.lt', 'PS8@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_8', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-44665544000E', 'PS9', 'PS9', 'PS9@viko.lt', 'PS9@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_9', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-44665544000F', 'PS10', 'PS10', 'PS10@viko.lt', 'PS10@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_10', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440010', 'PS11', 'PS11', 'PS11@viko.lt', 'PS11@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_11', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440011', 'PS12', 'PS12', 'PS12@viko.lt', 'PS12@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_12', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440012', 'PS13', 'PS13', 'PS13@viko.lt', 'PS13@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_13', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440013', 'PS14', 'PS14', 'PS14@viko.lt', 'PS14@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_14', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440014', 'PS15', 'PS15', 'PS15@viko.lt', 'PS15@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_15', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440015', 'PS16', 'PS16', 'PS16@viko.lt', 'PS16@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_16', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440016', 'PS17', 'PS17', 'PS17@viko.lt', 'PS17@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_17', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440017', 'PS18', 'PS18', 'PS18@viko.lt', 'PS18@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_18', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440018', 'PS19', 'PS19', 'PS19@viko.lt', 'PS19@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_19', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA=='),
                                                                                                                                                                                                               ('550e8400-e29b-41d4-a716-446655440019', 'PS20', 'PS20', 'PS20@viko.lt', 'PS20@VIKO.LT', 1, 0, 0, 0, 0, 'security_stamp_20', 'AQAAAAIAAYagAAAAEC59W/iKfa4pl0A6aoJ88TiDMrTyoncV0yV9qBNKIbuPOd4mK3NQ9UUQ9u4aKxaxtA==');

-- Seed User-Role association for Reviewers
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
                                                 ('550e8400-e29b-41d4-a716-446655440006', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440007', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440008', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440009', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-44665544000A', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-44665544000B', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-44665544000C', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-44665544000D', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-44665544000E', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-44665544000F', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440010', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440011', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440012', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440013', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440014', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440015', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440016', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440017', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440018', '550e8400-e29b-41d4-a716-446655440001'),
                                                 ('550e8400-e29b-41d4-a716-446655440019', '550e8400-e29b-41d4-a716-446655440001');

COMMIT;


