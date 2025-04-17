describe('Accepting GDPR | Showing content', () => {
    it('Visit page, click "Accepteren" and page contains name', () => {
        cy.visit('http://localhost:8080/')

        cy.get('.gdpr-consent__button--accept').click();

        cy.contains('Erik Leusink');
    })
})

describe('Decline GDPR | Hiding content', () => {
    it('Visit page, click "Afwijzen" and page contains name', () => {
        cy.visit('http://localhost:8080/')

        cy.get('.gdpr-consent__button--reject').click();

        cy.contains('Helaas kan u deze content niet bezichtigen, in verband met de privacy van de ontwikkelaar.');
    })
})

