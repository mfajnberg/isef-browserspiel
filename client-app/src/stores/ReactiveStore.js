import { defineStore } from 'pinia' 

export const useStore = defineStore('ReactiveStore', {
    id: 'MyStoreId',
    state: () => ({
        isRegFormVisible: false,
    }),
    getters: {
        getIsRegFormVisible: (state) => state.isRegFormVisible,
    },
    actions: {

        toggleRegForm() {
            this.isRegFormVisible = !this.isRegFormVisible
            console.log(this.isRegFormVisible)
        },
    },
})