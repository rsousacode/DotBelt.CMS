export type ArrayElement = {
  id: number | string;
  [key: string]: unknown;
};

export const ArrayUtils = {
  getNextElement: (array: ArrayElement[], id: number | string): ArrayElement | undefined => {
    const index = array.findIndex(el => el.id === id);
    if (index !== -1 && index < array.length - 1) {
      return array[index + 1];
    }
    return undefined;
  },

  getPreviousElement: (array: ArrayElement[], id: number | string): ArrayElement | undefined => {
    const index = array.findIndex(el => el.id === id);
    if (index > 0) {
      return array[index - 1];
    }
    return undefined;
  }
};