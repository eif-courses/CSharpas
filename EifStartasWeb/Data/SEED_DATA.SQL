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
     'hashed_password');

-- Seed User-Role association
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
    ('550e8400-e29b-41d4-a716-446655440005', '550e8400-e29b-41d4-a716-446655440000');
