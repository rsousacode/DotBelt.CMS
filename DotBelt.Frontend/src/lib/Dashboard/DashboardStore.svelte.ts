// src/stores/dashboardStore.ts

import {writable} from 'svelte/store';
import type {Snippet} from "svelte";


interface DashboardData {
    title: string;
    subtitle: string;
    dashboardFragment : Snippet | undefined;
}

const dashboardData = writable<DashboardData>({ title: '', subtitle: '', dashboardFragment: undefined });

function updateDashboardData(data: { title: string; subtitle: string; }) {
    dashboardData.update(currentData => {
        return {
            ...currentData,
            title: data.title,
            subtitle: data.subtitle
        };
    });
}

function setDashboardData(data: { title: string; subtitle: string; }) {
    dashboardData.set({
            dashboardFragment: undefined,
            title: data.title,
            subtitle: data.subtitle
    });
}


function updateDashboardFragment(fragment: any) {
    dashboardData.update(currentData => {
        return {
            ...currentData,
            dashboardFragment: fragment
        };
    });

}

// Export the store and the methods
export { dashboardData, updateDashboardData, setDashboardData, updateDashboardFragment};