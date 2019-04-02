CREATE TABLE [dbo].[Carrier]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CarrierName] NVARCHAR(255) NOT NULL, 
    [CarrierCode] NVARCHAR(255) NOT NULL, 
    [Phone] NVARCHAR(30) NOT NULL, 
    [CarrierLogo] NVARCHAR(MAX) NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    [SubscriptionStatusId] INT NOT NULL, 
    CONSTRAINT [FK_Carrier_ToSubscriptionStatus] FOREIGN KEY ([SubscriptionStatusId]) REFERENCES [SubscriptionStatus]([Id]), 
)
