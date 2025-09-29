export type DateFilterInput = {
  filter: {
    dayOfWeek: string;
  }
};

export type CarTypeFilterInput = {
  filter: {
    id: number;
    calendarDate: string;
    type: string;
  }
};

export type SeatListFilterInput = {
  filter: {
    id: number;
    calendarDate: string;
    n: number;
  }
};
