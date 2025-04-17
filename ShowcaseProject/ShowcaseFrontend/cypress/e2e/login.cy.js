describe('Foutief Inloggen | Foutmelding inloggen mislukt', () => {
    it('Visit page, Fill form incorrect and page contains Login mislukt', () => {
        cy.visit('http://localhost:8080/login')

        cy.get('#username').type('test@gmail.com')
        cy.get('input[type="password"]').type('test123');

        cy.get('button[type="submit"]').click();

        cy.contains('Login mislukt!');
    })
})

describe('Correct Inloggen | Email + Uitloggen staat in navbar', () => {
    it('Visit page, click "Afwijzen" and page contains name', () => {
        cy.visit('http://localhost:8080/login')

        cy.get('#username').type('test@gmail.com')
        cy.get('input[type="password"]').type('P@ssw0rd');

        cy.get('button[type="submit"]').click();

        cy.contains('test@gmail.com');
        cy.contains('Uitloggen');

        cy.url().should('eq', 'http://localhost:8080/')
    })
})