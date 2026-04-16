USE TalentInsights

CREATE TABLE EmailTemplates(
	EmailTemplateId INT IDENTITY(1,1) NOT NULL,
	Name VARCHAR(100) NOT NULL,
	Subject VARCHAR(255) NOT NULL,
	Body TEXT NOT NULL,
	CreateAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

INSERT INTO EmailTemplates(Name, Subject, Body)
VALUES
	('COLLABORATOR_REGISTER','Registro de usuario - Talent Insights', 'Se creó una cuenta con su correo electrónico. Su contraseña es: <strong>{{password}}</strong>'),
	('AUTH_LOGIN_SUCCESS', 'Inicio de sesión exitoso - Talent Insights', 'Se inició sesión en su cuenta a las: <strong>{{datetime}}</strong>'),
	('AUTH_LOGIN_FAILED', 'Inicio de sesión fallido - Talent Insights', 'Se intentó iniciar sesión en su cuenta, si no fue usted quien realizó esta acción, comuníquelo de inmediato a un administrador.')
GO