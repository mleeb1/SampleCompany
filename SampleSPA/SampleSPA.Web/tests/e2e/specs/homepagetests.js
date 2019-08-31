// https://docs.cypress.io/api/introduction/api.html

describe('Home Page Tests', function () {
  // beforeEach(function () {
  //  cy.request('GET', 'http://localhost:8081/api/products')
  // })

  it('Visits the app root url', () => {
    cy.visit('/')
    cy.contains('h1', 'Welcome to Your Vue.js App')
  })
})
