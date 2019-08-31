import { shallowMount } from '@vue/test-utils'
import Blogs from '@/views/Blogs.vue'
import axios from 'axios'

jest.mock('axios')

describe('getBlogs tests', () => {
  it('sets the returned blogs to the blogs prop on the model', async () => {
    const expectedBlogs = [{ 'id': 1, 'url': 'http://www.company.com' }]
    axios.get.mockImplementation(() => Promise.resolve({ status: 200, data: expectedBlogs }))
    const wrapper = shallowMount(Blogs, {
      data () {
        return {
          blogs: []
        }
      }
    })

    await wrapper.vm.getBlogs()
    expect(axios.get).toBeCalled()
    expect(wrapper.vm.$data.blogs.length).toEqual(1)
    expect(wrapper.vm.$data.blogs).toEqual(expectedBlogs)
  })
})
