// src/stores/dashboardStore.ts

import { writable } from 'svelte/store';

interface DashboardData {
  title: string;
  subtitle: string;
  dashboardFragment: any;
}

const dashboardData = writable<DashboardData>({
  title: '',
  subtitle: '',
  dashboardFragment: null,
});

function updateDashboardData(data: { title: string; subtitle: string }) {
  dashboardData.update((currentData) => {
    return {
      ...currentData,
      title: data.title,
      subtitle: data.subtitle,
    };
  });
}

function setDashboardData(data: { title: string; subtitle: string }) {
  dashboardData.set({
    dashboardFragment: undefined,
    title: data.title,
    subtitle: data.subtitle,
  });
}

function updateDashboardFragment(fragment: any) {
  dashboardData.update((currentData) => {
    return {
      ...currentData,
      dashboardFragment: fragment,
    };
  });
}

// Export the store and the methods
export {
  dashboardData,
  updateDashboardData,
  setDashboardData,
  updateDashboardFragment,
};
