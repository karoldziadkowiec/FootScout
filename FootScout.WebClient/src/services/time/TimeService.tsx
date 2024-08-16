import { format } from 'date-fns';

const TimeService = {
  formatDateToEURWithHour(dateString: string): string {
    const date = new Date(dateString);
    return format(date, 'dd.MM.yyyy HH:mm:ss');
  },

  formatDateToEUR(dateString: string): string {
    const date = new Date(dateString);
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}.${month}.${year}`;
  },

  formatDateToForm(dateString: string): string {
    const date = new Date(dateString);
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${year}-${month}-${day}`;
  },

  calculateSkippedDays(endDate: string): number {
    const currentDate = new Date();
    const end = new Date(endDate);
    const timeDiff = currentDate.getTime() - end.getTime();
    const days = timeDiff / (1000 * 3600 * 24);
    return Math.floor(days);
  },

  calculateDaysLeft(endDate: string): number {
    const currentDate = new Date();
    const end = new Date(endDate);
    const timeDiff = end.getTime() - currentDate.getTime();
    const daysLeft = timeDiff / (1000 * 3600 * 24);
    return Math.ceil(daysLeft);
  },

  calculateDaysLeftPassed(endDate: string): string {
    const currentDate = new Date();
    const end = new Date(endDate);

    if (currentDate <= end) {
        const timeDiff = end.getTime() - currentDate.getTime();
        const daysLeft = timeDiff / (1000 * 3600 * 24);
        return `${Math.ceil(daysLeft)} days left`;
    }
    else {
        const timeDiff = currentDate.getTime() - end.getTime();
        const days = timeDiff / (1000 * 3600 * 24);
        return `${Math.floor(days)} days passed`;
    }
}
};

export default TimeService;