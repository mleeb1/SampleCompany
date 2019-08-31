// https://docs.cypress.io/api/introduction/api.html

describe('Blog Page Tests', function () {
  it('Adding a new Blog Works', () => {
    // Give an alias to request
    cy.server().route('GET', '/api/blogs').as('getBlogsCall')
    cy.server().route('POST', '/api/blogs').as('postBlogsCall')

    cy.visit('/blogs')
    cy.get('#newBlogUrl').type('http://www.company.com')
    cy.get('#saveBlog').click()

    // Wait for response.status to be 200
    cy.wait('@postBlogsCall').its('status').should('be', 200)

    cy.get('#getBlogs').click()

    // Wait for response.status to be 200
    cy.wait('@getBlogsCall').its('status').should('be', 200)

    cy.contains('body', 'http://www.company.com')
  })
})
