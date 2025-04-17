describe('Game Pagina (niet ingelogd) | Redirect naar inlog pagina', () => {
    it('Visit game page, gets redirected to inlog page', () => {
        cy.visit('http://localhost:8080/game')
        cy.url().should('contain', 'http://localhost:8080/Login/')


       
    })
})

describe('Game Pagina (ingelogd) | Toont game pagina', () => {
    it('Login, Visit game page, shows game page', () => {
        cy.visit('http://localhost:8080/login')

        cy.get('#username').type('test@gmail.com')
        cy.get('input[type="password"]').type('P@ssw0rd');

        cy.get('button[type="submit"]').click();

        cy.contains('test@gmail.com');
        cy.contains('Uitloggen');

        cy.visit('http://localhost:8080/game')
        cy.url().should('eq', 'http://localhost:8080/game')
    })
})