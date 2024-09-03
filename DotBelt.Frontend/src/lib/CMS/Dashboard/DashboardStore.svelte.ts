// src/stores/dashboardStore.ts

import { writable } from 'svelte/store';


export let dashData : DashboardData = $state({ title: '', subtitle: '', dashboardFragment: null })

interface DashboardData {
    title: string;
    subtitle: string;
    dashboardFragment : any;
}

const dashboardData = writable<DashboardData>({ title: '', subtitle: '', dashboardFragment: null });

function setDashboardData(data: { title: string; subtitle: string; }) {
    dashboardData.update(currentData => {
        let newData = {
            ...currentData,
            title: data.title,
            subtitle: data.subtitle
        };
        return newData;
    });
}
function setDashboardFragment(fragment: any) {
    dashboardData.update(currentData => {
        let newData = {
            ...currentData,
            dashboardFragment: fragment
        };
        return newData;
    });

}

// Export the store and the methods
export { dashboardData, setDashboardData, setDashboardFragment};