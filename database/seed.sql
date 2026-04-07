-- Seed Data for Bugs Table
INSERT INTO "Bugs" ("Title", "Description", "Status", "CreatedAt", "UpdatedAt")
VALUES 
('Login button not responsive', 'Clicking the login button on the home page does nothing.', 'Open', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
('Update profile image fails', 'When uploading a new profile image, it returns 500 error.', 'WorkInProgress', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
('Wrong discount calculation', 'Discount is applied twice during checkout.', 'Open', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
('Menu overlaps on mobile', 'Hamburger menu is hidden behind the header on iPhone 13.', 'Hold', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
('Export to PDF timeout', 'Exporting a report with 1000+ records times out.', 'Rejected', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
('Typo in footer', 'Footer says "Copiright" instead of "Copyright".', 'Closed', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
('Broken link in welcome email', 'The "Confirm Email" button points to localhost.', 'WorkInProgress', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
('Dark mode toggle lag', 'Switching to dark mode takes more than 2 seconds.', 'Open', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
